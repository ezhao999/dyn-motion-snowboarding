using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(inputData))]
public class PhysicsTest : MonoBehaviour
{
    public float force;
    public ForceMode forceMode;

    private Rigidbody rb;
    private Vector3 inputForceDir = new Vector3(0,0,0);
    private inputData _inputData; // for VR controller rotation input

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _inputData = GetComponent<inputData>(); // for VR controller rotation input
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTilt()
    {
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rightQuat))
        {
            Vector3 rightInput = rightQuat.eulerAngles;
            rightInput.Normalize();
            inputForceDir = new Vector3(0, 0, rightInput.z);
            Vector3 globalForceDir = gameObject.transform.TransformDirection(inputForceDir);
            rb.AddForce(globalForceDir * force, forceMode);
        }
    }

}
