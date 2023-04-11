using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(Rigidbody))]
public class VectorRotation : MonoBehaviour
{
    // source: https://gist.github.com/fasiha/6c331b158d4c40509bd180c5e64f7924
    public float gravitationalForce = 9.81f;
    public ForceMode forceMode;

    private Rigidbody rb;
    private inputData _inputData; // for VR controller rotation input
    private Vector3 gravity = new Vector3(0, -1, 0);
    private Vector3 globalForceDir = new Vector3(0, -1, 0);
    private float theta = 0f;
    private Vector3 planeNormal = new Vector3(0, 1, 0);

    [SerializeField, Range(-90, 90)]
    private float debugRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _inputData = GetComponent<inputData>(); // for VR controller rotation input
    }

    private Vector3[] xProjectV(Vector3 x, Vector3 v)
    {
        Vector3[] res = new Vector3[2];
        res[0] = Vector3.Dot(x, v) / Vector3.Dot(v, v) * v;
        res[1] = x - res[0];
        return res;
    }

    private Vector3 RotateAbout(Vector3 toRotate, Vector3 about, float theta)
    {
        Vector3[] proj = xProjectV(toRotate, about);
        Vector3 w = Vector3.Cross(about, proj[1]);
        return proj[0] +
               proj[1]*Mathf.Cos(theta) +
               Vector3.Magnitude(proj[1]) * (w/Vector3.Magnitude(w)) * Mathf.Sin(theta);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // first project gravitational acceleration to plane
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hitInfo, 2.5f))
        {
            planeNormal = hitInfo.normal;
            globalForceDir = Vector3.ProjectOnPlane(gravity, planeNormal);
            Debug.Log("Projected force: " + globalForceDir);
        } else globalForceDir = new Vector3(0, -1, 0); // in air, force down

        // now rotate vector about plane normal with angle determined by input
#if UNITY_EDITOR // Used to manually set rotation while in editor, is ignored on build
        if (debugRotation != 0)
        {
            //theta = Mathf.Clamp(debugRotation, -90, 90);
            theta = debugRotation / 2;
            globalForceDir = RotateAbout(globalForceDir, planeNormal, theta);
        }
#else
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rightQuat))
            {
                Vector3 rightInput = rightQuat.eulerAngles;
                //theta = Mathf.Clamp(rightInput.z, -90, 90);
                theta = debugRotation / 2;
                globalForceDir = RotateAbout(globalForceDir, planeNormal, theta);
            }
#endif
        // apply force
        rb.AddForce(globalForceDir * gravitationalForce, forceMode);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hitInfo, 2.5f))
        {
            Gizmos.DrawRay(this.transform.position, Vector3.ProjectOnPlane(globalForceDir, hitInfo.normal) * 10f);
        }
    }
}
