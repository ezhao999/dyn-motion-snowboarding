using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;


public class Calibration : MonoBehaviour
{
    // Main: Calibrate range of motion for the controller
    // Secondary: show calibration state
    // Secondary: toggle physics only once range is set

    // Needed objects
    private inputData _inputData;
    private SnowboardVehicle board;
    public Text output;
    public calibrationBars UI_Bars;


    // vars
    public float? lBound = null;
    public float? rBound = null;
    public float? range = null;
    public float rawTheta;
    public bool isCalibrated;


    void Start()
    {
        _inputData = GetComponent<inputData>();
        board = GetComponent<SnowboardVehicle>();

    }

    void Update()
    {
        rawTheta = board.rightInput.z;
        displayRange(rawTheta);
    }

    void displayRange(float angle)
    {
        string outText = "";
        // message if nothing is calibrated
        // $"Tilt calibration from {lBound} to {rBound}"
        if (lBound == null)
        {
            outText = $"Press 'A' to set left tilt to {angle}";
        } else if (rBound == null)
        {
            outText = $"Press 'A' to set right tilt to {angle}";
        } else if (!isCalibrated)
        {
            outText = $"Board Ready!\nTilt calibration from {lBound} to {rBound}";
        }

        output.text = outText;
    }

    void OnReset() // sets bounds using right controller button, and left controller tilt
    {
        Debug.Log("pressed");
        if (lBound == null || rBound == null)
        {
            UI_Bars.setBar(UI_Bars.target);
        }
        // If lBound is null, set lBound
        if (lBound == null)
        {
            if (rawTheta != 0)
            {
                lBound = rawTheta;
            }
        } else if (rBound == null)
        {
            if (rawTheta != 0
                && rawTheta != lBound)
            {
                rBound = rawTheta;
                range = rBound - lBound;
            }
        } else
        {
            isCalibrated = true;
        }
        // Elif rBound is null, set rBound
        // Else: set isKinematic of the rigidbody to false
    }

    void OnBack() //recalibrate bounds
    {
        lBound = null;
        rBound = null;
        range = null;
        isCalibrated = false;
        UI_Bars.resetState();
}
}
