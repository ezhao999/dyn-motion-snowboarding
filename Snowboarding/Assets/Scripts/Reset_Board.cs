using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Reset_Board : MonoBehaviour
{
    public Vector3 startPos;
    public Quaternion startRot;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        rb.velocity = new Vector3(0, 0, 0);
        gameObject.transform.position = startPos;
        gameObject.transform.rotation = startRot;
    }
}
