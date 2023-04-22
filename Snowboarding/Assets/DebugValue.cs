using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Take an input value, put it as a label and value on UI canvas
public class DebugValue : MonoBehaviour
{
    [SerializeField] SnowboardVehicle input;
    [SerializeField] string label;
    private Text output;
    private float controllerAngle;
    private Vector3 controllerRaw;
    // Start is called before the first frame update
    void Start()
    {
        output = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        controllerRaw = input.rightInput;
        controllerAngle = input.theta;
        string outText = $"{label}: {controllerRaw}";
        output.text = outText;
    }
}
