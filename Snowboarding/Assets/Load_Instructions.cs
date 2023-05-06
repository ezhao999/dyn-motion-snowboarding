using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_Instructions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(instructionsLoad());
    }

    public IEnumerator instructionsLoad(){
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(2);
    }
}
