using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class exposedSoundUpdate : MonoBehaviour
{
    public AudioMixer boardMixer;

    [Range(0.0f, 50.0f)] public float velocity;
    private float freqMax = 3.0f;
    private float freqMin = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateEnvSounds();
    }

    void updateEnvSounds()
    {
        float snowGain = scale(0, 50.0f, freqMin, freqMax, velocity);
        boardMixer.SetFloat("snowSound", snowGain);
    }

    float scale(float a, float b, float c, float d, float oldVal)
    {
        float oldRange = b - a;
        float newRange = d - c;
        float proportion = oldVal / oldRange;
        float newVal = proportion * newRange;
        return newVal;
    }
}
