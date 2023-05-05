using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Reset_Board : MonoBehaviour
{
    public Vector3 startPos;
    public Quaternion startRot;
    public InitialBoost boost;
    private Rigidbody rb;
    private Calibration calibration;
    private SnowboardVehicle snowboardVehicleScript;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        calibration = GetComponent<Calibration>();
        boost = GetComponent<InitialBoost>();
        startPos = gameObject.transform.position;
        startRot = gameObject.transform.rotation;
        snowboardVehicleScript = GetComponent<SnowboardVehicle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!calibration.isCalibrated)
        {
            rb.velocity = new Vector3(0, 0, 0);
            gameObject.transform.position = startPos;
            gameObject.transform.rotation = startRot;
        }
    }

    void OnReset()
    {
        Debug.Log("reset");
        if (calibration.isCalibrated)
        {
            boost.isBoosted = false;
            boost.boostLeft = boost.duration;
        }
        rb.velocity = new Vector3(0, 0, 0);
        gameObject.transform.position = startPos;
        gameObject.transform.rotation = startRot;
        //rb.isKinematic = false;
        //snowboardVehicleScript.myState = GameState.playing;
    }
}
