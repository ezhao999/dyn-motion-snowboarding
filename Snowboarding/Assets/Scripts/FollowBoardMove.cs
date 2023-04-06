using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBoardMove : MonoBehaviour
{
    public float smoothFrames;
    int elapsedFrames;

    [SerializeField]
    private SnowboardWheel snowboardWheelObject;

    private Transform snowboardWheelTransform;

    private void Start()
    {
        snowboardWheelTransform = snowboardWheelObject.gameObject.transform;
    }

    private void FixedUpdate()
    {
        float t = (float)elapsedFrames / smoothFrames;
        Vector3 newPosition = Vector3.Lerp(this.transform.position, snowboardWheelTransform.position, t);

        // Currently unused
        Quaternion wheelRotation = Quaternion.Slerp(this.transform.rotation, snowboardWheelTransform.rotation, t);

        // Rotate to surface normal
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hitInfo, 5))
        {
            this.transform.rotation = hitInfo.transform.rotation * Quaternion.LookRotation(hitInfo.normal);
        }
        this.transform.rotation = Quaternion.Euler(this.transform.eulerAngles.x + 90f, snowboardWheelTransform.eulerAngles.y + 90f, this.transform.rotation.eulerAngles.z) ;
        this.transform.position = newPosition;
        elapsedFrames = (int)((elapsedFrames + 1) % (smoothFrames + 1));
    }
}
