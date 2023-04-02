using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class joystickTilt : MonoBehaviour
{
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

    }

    private void OnTilt(InputValue value)
    {
        rawVal = value.Get<Vector2>();
        float rawX = rawVal.x;
        Debug.Log(rawX);
        gameObject.transform.eulerAngles = new Vector3(0, 0, (rawX * tiltFactor));
    }
}