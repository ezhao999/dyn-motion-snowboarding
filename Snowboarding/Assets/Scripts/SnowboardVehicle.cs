using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class SnowboardVehicle : MonoBehaviour
{
    [Range(-1.0f, 1.0f)] public float wheelAngle;
    [Range(10.0f, 40.0f)] public float maxSteer;


    [SerializeField] private WheelCollider frontWheel;
    [SerializeField] private WheelCollider backWheel;
    [Range(0.0f, 8.0f)] public float maxFriction; // 0-1 for stiffness, 0-5 for asymptote

    private float minVelocity = 2.0f;

    [SerializeField] private GameObject frontMesh;
    [SerializeField] private GameObject backMesh;
    [SerializeField] private Text DebugText;

    [SerializeField] private float maxAngle;

    public Vector3 currentVisualRotation;
    public float currentWheelRotation = 0;

    private inputData _inputData; // for VR controller rotation input
    private Vector3 inputForceDir = new Vector3(0, 0, 0);
    private Rigidbody rb;


    void Start()
    {
        _inputData = GetComponent<inputData>(); // for VR controller rotation input
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //V1:

        float mappedSteer = wheelAngle * maxSteer;

        //get current angle
        currentVisualRotation = frontMesh.transform.localRotation.eulerAngles;

        //set rotation as new Vector3(currentX, newY, currentZ);
        frontMesh.transform.localRotation = Quaternion.Euler(
                                            new Vector3(currentVisualRotation.x, 
                                                        mappedSteer, 
                                                        currentVisualRotation.z));
        //steer front wheel collider
        frontWheel.steerAngle = mappedSteer;


        // V2:
        // Goal: variable friction
        // Logic:
        //      Rotation recorded as var
        //      Rotation applied to mesh and wheel collider
        //      Change friction based on wheel angle

        // Set stiffness equal to steering angle range
        WheelFrictionCurve fFriction = frontWheel.sidewaysFriction;
        WheelFrictionCurve rFriction = backWheel.sidewaysFriction;

        // V2.5:
        // Goal: Asymptote friction changes, not stiffness

        float frictionVal = Mathf.Abs(wheelAngle * maxFriction);
        frictionVal = Mathf.Max(frictionVal, maxFriction / 2);

        fFriction.asymptoteValue = frictionVal;
        rFriction.asymptoteValue = frictionVal;
        frontWheel.sidewaysFriction = fFriction;
        backWheel.sidewaysFriction = rFriction;


        // V4:
        // Goal: Constant minimum speed
        if(rb.velocity.x < minVelocity)
        {
            Debug.Log("adding force");
            //rb.AddForce(new Vector3(5, 0, 0));
            rb.AddRelativeForce(Vector3.forward * 200);
        }

    }
    // V3:
    // Goal: Limit Player Rotation
    //      Current progress:
    //      Raising wheel stiffness in the rear helps prevent oversteer / spinning out
    //      No need to add scripts for now

    float scale(float a, float b, float c, float d, float oldVal)
    {
        float oldRange = b - a;
        float newRange = d - c;
        float proportion = oldVal / oldRange;
        float newVal = proportion * newRange;
        return newVal;
    }

}
