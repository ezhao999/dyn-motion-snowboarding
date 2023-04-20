using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class SnowboardWheel : MonoBehaviour
{
    [SerializeField]
    private WheelCollider snowboardWheel;
    private inputData _inputData; // for VR controller rotation input
    private Vector3 inputForceDir = new Vector3(0, 0, 0);   

    [SerializeField]
    private GameObject wheelCylinder;
    [SerializeField]
    private Text DebugText;

    public Vector3 currentVisualRotation;
    public float currentWheelRotation = 0;

    void Start()
    {
        _inputData = GetComponent<inputData>(); // for VR controller rotation input
    }

    private void Update()
    {
/*        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rightQuat))
        {
            Vector3 rightInput = rightQuat.eulerAngles;
            currentWheelRotation = rightInput.z;

        }*/
        // DebugText.text = currentWheelRotation.ToString();
        currentVisualRotation = wheelCylinder.transform.localRotation.eulerAngles;
        // snowboardWheel.steerAngle = currentWheelRotation; // used for controller input first
        currentWheelRotation = currentVisualRotation.y;
        snowboardWheel.steerAngle = currentWheelRotation;

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
