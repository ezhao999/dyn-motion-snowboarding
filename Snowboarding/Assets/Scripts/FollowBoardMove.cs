using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBoardMove : MonoBehaviour
{
    public float smoothFrames;
    int elapsedFrames;

    [SerializeField]
    private SnowboardWheel snowboardWheel;

    private Transform snowboardWheelTransform;

    private void Start()
    {
        snowboardWheelTransform = snowboardWheel.gameObject.transform;
    }

    private void FixedUpdate()
    {
        float t = (float)elapsedFrames / smoothFrames;
        Vector3 newPosition = Vector3.Lerp(this.transform.position, snowboardWheelTransform.position, t);

        Quaternion wheelRotation = Quaternion.Slerp(this.transform.rotation, snowboardWheelTransform.rotation, t);
        // Rotate to surface normal
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hitInfo, 5))
        {
            this.transform.rotation = hitInfo.transform.rotation * Quaternion.LookRotation(hitInfo.normal);
        } // else if not hit what do we do?? (in air??)
        //Debug.Log("Normal rotation: " + this.transform.rotation);
        this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z);
        //this.transform.rotation = newRotation;
        this.transform.position = newPosition;
        elapsedFrames = (int)((elapsedFrames + 1) % (smoothFrames + 1));
    }
}
