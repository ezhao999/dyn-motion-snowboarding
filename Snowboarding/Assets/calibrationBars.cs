using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class calibrationBars : MonoBehaviour
{
    // Start is called before the first frame update
    public Calibration calibration;
    public Image calibrationScreen;
    public Image calibrationL;
    public Image calibrationR;
    public Image target; // which bar to set? L or R
    public Sprite screen1;
    public Sprite screen2;


    private Color32 finished = new Color32(46, 197, 25, 255);
    private Color pending = new Color(255, 255, 255, 0.3f);
    private float midValue = 195f;
    private float maxValue = 30f;
    void Start()
    {
        resetState();

    }

    // Update is called once per frame
    void Update()
    {
        // if is calibrated, show nothing
        // else if left side calibrated, show right side calibration image
        // else if right side null, 
        if (calibration.isCalibrated)
        {
            calibrationScreen.enabled = false;
            calibrationL.enabled = false;
            calibrationR.enabled = false;
            calibrationL.fillAmount = 0;
            calibrationR.fillAmount = 0;
            return;
        }
        else if (calibration.lBound == null)
        {
            calibrationScreen.sprite = screen1;
            target = calibrationL;
        }
        else if (calibration.rBound == null)
        {
            calibrationScreen.sprite = screen2;
            target = calibrationR;
        }
        else
        {
            return;
        }
        adjustBar(target);
    }


    public void setBar(Image target)
    {
        // set color
        target.color = finished;

    }

    void adjustBar(Image target)
    {
        float proportion;
        // change fill amount according to angle
        if (target == calibrationL)
        {
            proportion = Mathf.Clamp((calibration.rawTheta - midValue) / maxValue, 0f, 1f);
        } else
        {
            proportion = Mathf.Clamp((midValue - calibration.rawTheta) / maxValue, 0f, 1f);
        }


        target.fillAmount = proportion;
    }

    public void resetState()
    {
        calibrationScreen.enabled = true;
        calibrationL.enabled = true;
        calibrationR.enabled = true;
        calibrationScreen.sprite = screen1;
        calibrationL.fillAmount = 0;
        calibrationR.fillAmount = 0;
        calibrationL.color = pending;
        calibrationR.color = pending;
        target = calibrationL;
    }

    float scale(float a, float b, float c, float d, float oldVal)
    {
        float oldRange = b - a;
        float newRange = d - c;
        float proportion = (oldVal - a) / oldRange;
        float newVal = c + (proportion * newRange);
        return newVal;
    }
//cutscene stuff
    // public void Play(){
    //     PlayableDirector.Play();
        
    // }

}
