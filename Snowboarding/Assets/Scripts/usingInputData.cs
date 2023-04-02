using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(inputData))]
public class usingInputData : MonoBehaviour
{
    private inputData _inputData;

    // Start is called before the first frame update
    void Start()
    {
        _inputData = GetComponent<inputData>();
    }

    // if want to constantly tilt, either use UPDATE or in tilt action add deviceAngularVelocity

    // Update is called once per frame
    //void Update()
    //{
    //    if (_inputData._rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rightQuat))
    //    {
    //        Vector3 rightV3 = rightQuat.eulerAngles;
    //        gameObject.transform.eulerAngles = new Vector3(0, 0, rightV3.z);
    //    }
    //}

    private void OnTilt()
    {
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rightQuat))
        {
            Vector3 rightV3 = rightQuat.eulerAngles;
            gameObject.transform.eulerAngles = new Vector3(0, 0, rightV3.z);
        }
    }
}
