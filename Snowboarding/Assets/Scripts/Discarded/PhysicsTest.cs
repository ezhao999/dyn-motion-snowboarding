using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsTest : MonoBehaviour
{
    [Header("Keep gravity on rigidbody off\n" +
            "Gravity is calculated in the script")]
    public float gravitationalForce = 9.81f;
    public float forceMultiplier = 1f;
    public float rotationSmoothSpeed = 1f;
    public ForceMode forceMode;

    private Rigidbody rb;
    private inputData _inputData; // for VR controller rotation input
    private Vector3 localForceDir = new Vector3(0, -1, 0);
    private Vector3 globalForceDir = new Vector3(0, -1, 0);


    [SerializeField, Range(-180, 180), Tooltip("Intended to simulate controller input")]
    private float debugRotation = 0f;
    [SerializeField]
    private float maxRotation = 90f;
    // value from 0-1 determining how rotated the controller is
    private float rotationFactor = 0f;

    // Object that gets created to track where the controller rotation is immediately set to
    // So the actual snowboard can "smooth" to this target
    private GameObject rotationObject;
    private float defaultRotationOffset;

    void Start()
    {
        rotationObject = new GameObject("RotationObject");
        rotationObject.transform.SetParent(transform, false);
        defaultRotationOffset = this.transform.localEulerAngles.y;

        rb = GetComponent<Rigidbody>();
        _inputData = GetComponent<inputData>(); // for VR controller rotation input
    }

    private void FixedUpdate()
    {
        // Used to manually set rotation while in editor, is ignored on build
#if UNITY_EDITOR
        if (debugRotation != 0)
        {
            // Slerp the rotation relative to input from debugRotation
            // This is to keep a smooth rotation that the velocity can be rotated with
            rotationObject.transform.rotation = Quaternion.Slerp(rotationObject.transform.rotation, Quaternion.Euler(0, debugRotation - defaultRotationOffset, 0), rotationSmoothSpeed);
            // rb.rotation = rotationObject.transform.rotation;
            rotationFactor = Mathf.Abs(debugRotation / maxRotation);
        }
#else
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rightQuat))
            {
                Vector3 rightInput = rightQuat.eulerAngles;
                rotationFactor = Mathf.Abs(rightInput.z / maxRotation);
                rotationObject.transform.rotation = Quaternion.Slerp(rotationObject.transform.rotation, Quaternion.Euler(0, rightInput.z - defaultRotationOffset, 0), rotationSmoothSpeed);
            }
#endif
        // now project to plane then apply force
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hitInfo, 2.5f))
        {
            float currentSlope = Vector3.Angle(Vector3.up, hitInfo.normal);
            float normalizedSlope = (currentSlope / 90);
            float rotationObjectY = Mathf.Abs(rotationObject.transform.localEulerAngles.y);
            localForceDir = new Vector3(0, 0, normalizedSlope);
            globalForceDir = rotationObject.transform.TransformDirection(localForceDir);

            // Flip the projected force direction if the projected force is pointing up the slope instead of down it
            Vector3 projectedForceDir = Vector3.ProjectOnPlane(globalForceDir, hitInfo.normal);
            if (projectedForceDir.y > 0 && !(rotationObjectY < 90 && rotationObjectY > 270))
            {
                Debug.Log("Trying to move up hill");
                projectedForceDir = -projectedForceDir;
            }

            rb.AddForce(projectedForceDir * forceMultiplier, forceMode);

        }
        else
        {
            Debug.Log("In air");
            rb.AddForce(Vector3.down * gravitationalForce, forceMode);
            return;
        }


        Vector3 currentRotationVector = new Vector3
            (
            rotationObject.transform.forward.x,
            rb.velocity.normalized.y,
            rotationObject.transform.forward.z
            );

        // Rotate the velocity to the vector of the rotation object for quick turning
        // (I think sometimes this adds energy to the system, which is not accurate)
        Vector3 newRotationVector = currentRotationVector;
        Debug.Log("Current rotation factor" + rotationFactor);
        Vector3 targetVelocity = Vector3.Lerp(rb.velocity.normalized, newRotationVector, rotationFactor);
        rb.velocity = targetVelocity * rb.velocity.magnitude;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {

        if (!Application.isPlaying) return;
        // Red is the force direction
        Gizmos.color = Color.red;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hitInfo, 2.5f))
        {
            Gizmos.DrawRay(this.transform.position, Vector3.ProjectOnPlane(globalForceDir, hitInfo.normal) * 10f);
        }
        // Blue is the current rotation
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(this.transform.position, rotationObject.transform.forward * 10f);
    }
#endif

    //private void OnTilt()
    //{
    //    if (_inputData._rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rightQuat))
    //    {
    //        Vector3 rightInput = rightQuat.eulerAngles;
    //        rightInput.Normalize();
    //        Vector3 inputForceDir = new Vector3(0, 0, rightInput.z);
    //        Vector3 globalForceDir = gameObject.transform.TransformDirection(inputForceDir);
    //        rb.AddForce(globalForceDir * gravitationalForce, forceMode);
    //    }
    //}

}
