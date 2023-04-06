using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartAfterTimer : MonoBehaviour
{
    public float restartTime = 1.0f;
    Vector3 startPos;
    Quaternion startRot;
    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
        startRot = this.transform.rotation;
        StartCoroutine(RestartAfterTime(restartTime));
    }

    IEnumerator RestartAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        this.transform.position = startPos;
        this.transform.rotation = startRot;
        StartCoroutine(RestartAfterTime(restartTime));

    }
}
