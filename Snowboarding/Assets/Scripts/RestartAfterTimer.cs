using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartAfterTimer : MonoBehaviour
{
    public float restartTime = 1.0f;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
        StartCoroutine(RestartAfterTime(restartTime));
    }

    IEnumerator RestartAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        this.transform.position = startPos;
        StartCoroutine(RestartAfterTime(restartTime));

    }
}
