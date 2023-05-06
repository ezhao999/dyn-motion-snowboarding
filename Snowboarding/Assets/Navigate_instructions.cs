using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigate_instructions : MonoBehaviour
{
    public GameObject NextButton;
    public GameObject BackButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex < 4)
        {
            NextPage();
        } else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void NextPage(){
        StartCoroutine(GoNext());
    }

    public void BackPage(){
        StartCoroutine(GoBack());
    }

    public IEnumerator GoNext(){
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator GoBack(){
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
