using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EndCutScene : MonoBehaviour
{
    public PlayableDirector PlayableDirector;

    public void Stop(){
        PlayableDirector.Stop();
        
    }
}
