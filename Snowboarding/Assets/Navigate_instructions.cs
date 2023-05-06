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
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            BackButton.SetActive(false);
        } else {
            BackButton.SetActive(true);
        }

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            NextButton.SetActive(false);
        } else {
            NextButton.SetActive(true);
        }
    }

    public void NextPage(){
        StartCoroutine(GoNext());
    }

    public void BackPage(){
        StartCoroutine(GoBack());
    }

    public IEnumerator GoNext(){
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator GoBack(){
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
