using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ButtonDown : MonoBehaviour
{
    public PlayableDirector PlayableDirector;

    public void Play(){
        PlayableDirector.Play();
        
    }
}
