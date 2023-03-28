using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class joystickTilt : MonoBehaviour
{
    // [SerializeField] //added by jessica
    // private LayerMask WhatIsGround;

    // [SerializeField] //added by jessica
    // private AnimationCurve animCurve;

    // [SerializeField] //added by jessica
    // private float Time;
    // //public AxisControl stickVert = stick.x;
    // //public AxisControl stickHor = stick.y;
    // // Start is called before the first frame update
    private Vector2 rawVal;
    public float tiltFactor = 25.0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       // SurfaceAlignment(); //added by jessica
    }

    private void OnTilt(InputValue value)
    {
        rawVal = value.Get<Vector2>();
        float rawX = rawVal.x;
        Debug.Log(rawX);
        gameObject.transform.eulerAngles = new Vector3(0, 0, (rawX * tiltFactor));
    }

    // private void SurfaceAlignment() //jessica following https://www.youtube.com/watch?v=Gt4NQDKc3tk&ab_channel=NULL
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