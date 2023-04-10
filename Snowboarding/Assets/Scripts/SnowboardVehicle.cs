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
    [Range(0.0f, 1.0f)] public float maxFriction;

    private inputData _inputData; // for VR controller rotation input
    private Vector3 inputForceDir = new Vector3(0, 0, 0);   

    [SerializeField] private GameObject frontMesh;
    [SerializeField] private GameObject backMesh;
    [SerializeField] private Text DebugText;

    public Vector3 currentVisualRotation;
    public float currentWheelRotation = 0;

    //For V2 - variable friction
    [Range(0.0f, 1.0f)] public float flatFriction;
    [Range(0.0f, 1.0f)] public float edgeFriction;

    void Start()
    {
        _inputData = GetComponent<inputData>(); // for VR controller rotation input
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

        //frontWheel.sidewaysFriction.stiffness = Math.Abs(wheelAngle * maxFriction);
        fFriction.stiffness = Mathf.Abs(wheelAngle * maxFriction);
        rFriction.stiffness = Mathf.Abs(wheelAngle * maxFriction);

        Debug.Log(fFriction.stiffness);

    }

    float scale(float a, float b, float c, float d, float oldVal)
    {
        float oldRange = b - a;
        float newRange = d - c;
        float proportion = oldVal / oldRange;
        float newVal = proportion * newRange;
        return newVal;
    }

    //private void OnTilt()
    //{
    //    if (_inputData._rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rightQuat))
    //    {
    //        Vector3 rightInput = rightQuat.eulerAngles;
    //        currentWheelRotation = rightInput.z;

    //    }
    //}

}
