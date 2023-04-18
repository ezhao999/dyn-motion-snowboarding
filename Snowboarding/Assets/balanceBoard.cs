using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balanceBoard : MonoBehaviour
{
    public Vector3 bodyRotation;
    public int smoothness = 5; // how many frames interpolation takes
    int elapsedFrames = 0;

    Rigidbody rb;
    float rbVelocityMagnitude;

    private Vector3 planeNormal;
    private Vector3 torqueForce;

    public SnowboardVehicle vehicleState;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // balance();
        rbVelocityMagnitude = rb.velocity.magnitude;
        rb.inertiaTensorRotation = Quaternion.identity;

        Stabilizer();
    }
    void Stabilizer()
    {
        // move towards plane normal
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hitInfo, 2.5f))
        {
            planeNormal = hitInfo.normal;
        }
        else planeNormal = Vector3.up;

        // adapted from Marcos-Schultz https://forum.unity.com/threads/balancing-motorcycle.611017/
        Vector3 axisFromRotate = Vector3.Cross(transform.up, Vector3.up);

        torqueForce = axisFromRotate.normalized * axisFromRotate.magnitude * 50;
        torqueForce.x = torqueForce.x * 0.4f;
        torqueForce -= rb.angularVelocity;
        //torqueForce.z = 0;
        Debug.Log(torqueForce);
        rb.AddTorque(torqueForce * rb.mass * 0.02f, ForceMode.Impulse);

/*        float rpmSign = Mathf.Sign(rb.velocity.x) * 0.02f;
        if (rbVelocityMagnitude > 1.0f)
        {
            rb.angularVelocity += new Vector3(0, vehicleState.wheelAngle * rpmSign, 0);
        }*/
    }
    void balance()
    {
        //interpolation doesn't work...very jittery

        // Set local z rotation to 0 every frame
        float interpolationRatio = (float)elapsedFrames / smoothness;
        bodyRotation = gameObject.transform.localRotation.eulerAngles;
        Vector3 balanced = new Vector3(bodyRotation.x, bodyRotation.y, 0);
        gameObject.transform.localRotation = Quaternion.Euler(
                                             Vector3.Lerp(bodyRotation, balanced, interpolationRatio));

        elapsedFrames = (elapsedFrames + 1) % (smoothness + 1);


    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hitInfo, 2.5f))
        {
            Gizmos.DrawRay(this.transform.position, Vector3.ProjectOnPlane(torqueForce, hitInfo.normal) * 10f);
            //Gizmos.DrawRay(this.transform.position, hitInfo.normal * 10f);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(this.transform.position, Vector3.up * 10f);

        }
    }
}
