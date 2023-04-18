using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartAfterTimer : MonoBehaviour
{
    [SerializeField] private float restartTime = 1.0f;
    [SerializeField] private Rigidbody rb;
    Vector3 startPos;
    Quaternion startRot;
    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
        startRot = this.transform.rotation;
        StartCoroutine(RestartAfterTime(restartTime));
    }
    void Update()
    {
        // Debug.Log(rb.velocity);

    }
    IEnumerator RestartAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        rb.velocity = new Vector3(0, 0, 0);
        this.transform.position = startPos;
        this.transform.rotation = startRot;
        StartCoroutine(RestartAfterTime(restartTime));

    }
}
