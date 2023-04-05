using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


///////////// DISCARD THIS CODE!!!! USE PhysicsTest.cs INSTEAD!!!!/////////

[RequireComponent(typeof(inputData))]
public class playerController : MonoBehaviour
{
    private inputData _inputData; // for VR controller rotation input
    private CharacterController controller;
    private bool groundedPlayer;
    private Vector3 localVelocity;
    private Vector3 movementVelocity;
    private float playerSpeed; // = velocity magnitude
    private float gravityValue = -9.81f; //-9.81
    private float inputAngle = 0f;
    private float downPreserveRatio = 0.3f;
    private float maxAngle = 90f; // make this smaller to make turn more sensitive

    // Start is called before the first frame update
    void Start()
    {
        _inputData = GetComponent<inputData>(); // for VR controller rotation input
        controller = gameObject.GetComponent<CharacterController>();
        // initialize velocity to 0 0 0
        localVelocity = new Vector3(0f, 0f, 0f);
    }

    // if want to constantly tilt, either use UPDATE or in tilt action add deviceAngularVelocity

    // Update is called once per frame
    void Update()
    {
        // groundedPlayer = controller.isGrounded; // maybe useful later?

        // apply gravitational acceleration, record new magnitude for this frame
        localVelocity.y += gravityValue * Time.deltaTime;
        playerSpeed = localVelocity.magnitude;

        // extract input
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rightQuat))
        {
            Vector3 rightV3 = rightQuat.eulerAngles;
            inputAngle = rightV3.z;
        }
        else { inputAngle = 0; }

        // calculate input impact and apply to localVelocity
        float horizontal = (1 - downPreserveRatio) * (inputAngle / maxAngle);
        float vertical = downPreserveRatio + (1 - downPreserveRatio) * (1 - (inputAngle / maxAngle));
        // notice that (horizontal, vertical, 0) is normalized
        localVelocity = new Vector3(horizontal * playerSpeed, vertical * playerSpeed, 0);

        // get actual movementVelocity projected on plane
        // where should the rayvast origin be???
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hitInfo, 5))
        {
            Vector3 globalVelocity = gameObject.transform.TransformDirection(localVelocity);
            movementVelocity = Vector3.ProjectOnPlane(globalVelocity, hitInfo.normal);
        } // else if not hit what do we do?? (in air??)

        // move player using character controller component
        controller.Move(movementVelocity * Time.deltaTime);
    }

    // LEGACY CODE:
    // last version had empty Update() and the function OnTilt() as written below:
    //private void OnTilt()
    //{
    //    if (_inputData._rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rightQuat))
    //    {
    //        Vector3 rightV3 = rightQuat.eulerAngles;
    //        Vector3 currV3 = gameObject.transform.eulerAngles;
    //        gameObject.transform.eulerAngles = new Vector3(currV3.x,
    //                                                       currV3.y,
    //                                                       rightV3.z);
    //    }
    //}
}
