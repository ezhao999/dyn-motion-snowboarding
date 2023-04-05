using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(inputData))]
public class PhysicsTest : MonoBehaviour
{
    public float gravitationalForce = 9.81f;
    public ForceMode forceMode;

    private Rigidbody rb;
    private inputData _inputData; // for VR controller rotation input
    private Vector3 localForceDir = new Vector3(0, -1, 0);
    private Vector3 globalForceDir = new Vector3(0, -1, 0);
    private float allocRatio = 0.9f;
    private float allocation = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _inputData = GetComponent<inputData>(); // for VR controller rotation input
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rightQuat))
        {
            Vector3 rightInput = rightQuat.eulerAngles;
            allocation = rightInput.z/180;
            localForceDir = new Vector3(allocRatio * allocation, -(1-allocRatio)-allocRatio*(1-Mathf.Abs(allocation)), 0);
            globalForceDir = gameObject.transform.TransformDirection(localForceDir);
        } else globalForceDir = new Vector3(0, -1, 0); // if no input only gravity

        // now project to plane then apply force
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hitInfo, 2))
        {
            Vector3 projectedForceDir = Vector3.ProjectOnPlane(globalForceDir, hitInfo.normal);
            rb.AddForce(projectedForceDir * gravitationalForce, forceMode);
        } else rb.AddForce(Vector3.down * gravitationalForce, forceMode); // if in air then only apply g
    }

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
