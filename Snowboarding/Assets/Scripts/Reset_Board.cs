using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Reset_Board : MonoBehaviour
{
    public Vector3 startPos;
    public Quaternion startRot;
    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.position;
        startRot = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnReset()
    {
        Debug.Log("reset");
        gameObject.transform.position = startPos;
        gameObject.transform.rotation = startRot;
    }
}
