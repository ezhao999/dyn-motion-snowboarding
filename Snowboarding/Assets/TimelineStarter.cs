using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineStarter : MonoBehaviour
{
    public GameObject yetiForRig;
    
    private void OnTriggerEnter(Collider other){
        PlayableDirector pd= GameObject.Find("yetiForRig").GetComponent<PlayableDirector>();
        if(pd!=null)
        {
            pd.Play();
        }
    }
}