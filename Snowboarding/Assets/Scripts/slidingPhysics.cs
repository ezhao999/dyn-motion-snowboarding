using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slidingPhysics : MonoBehaviour
{
    // [SerializeField]
    // private LayerMask WhatIsGround;

    // [SerializeField]
    // private AnimationCurve animCurve;

    // [SerializeField]
    // private float Time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // SurfaceAlignment();
    }

    // private void SurfaceAlignment() // following https://www.youtube.com/watch?v=Gt4NQDKc3tk&ab_channel=NULL
    // {
    //     Ray ray = new Ray(transform.position, -transform.up);
    //     RaycastHit info = new RaycastHit();
    //     Quaternion RotationRef = Quaternion.Euler(0, 0, 0);

    //     if (Physics.Raycast(ray, out info, WhatIsGround))
    //     {
    //         RotationRef = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, info.normal), animCurve.Evaluate(Time));
    //         transform.rotation = Quaternion.Euler(RotationRef.eulerAngles.x, transform.eulerAngles.y, RotationRef.eulerAngles.z);
    //     }
    //}
}
