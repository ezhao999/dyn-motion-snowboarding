using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelSteer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private WheelCollider snowboardWheel;
    private Vector3 inputForceDir = new Vector3(0, 0, 0);
    [SerializeField]
    private GameObject wheelCylinder;

    public float currentWheelRotation = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
