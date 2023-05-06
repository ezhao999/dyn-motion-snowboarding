using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIYetiDistance : MonoBehaviour
{
    public Calibration calibration;
    public emove yeti;
    public Image ui;
    public Sprite closeScreen;
    public Sprite farScreen;
    public TextMeshProUGUI number;
    // Start is called before the first frame update
    void Start()
    {
        number.text = "0M";
        ui.sprite = closeScreen;
        ui.enabled = false;
        number.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (calibration.isCalibrated && yeti.distance != 0)
        {
            ui.enabled = true;
            int output = (int)yeti.distance;
            if (output > 90)
            {
                ui.sprite = farScreen;
                number.text = ">90M";
            } else
            {
                ui.sprite = closeScreen;
                number.text = $"{output}M";
            }
        }
        else
        {
            ui.enabled = false;
            number.text = "";
        }

    }
}
