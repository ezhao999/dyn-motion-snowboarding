using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class exposedSoundUpdate : MonoBehaviour
{
    public AudioMixer boardMixer;
    public SnowboardVehicle player;

    //[Range(0.0f, 50.0f)] public float velocity;
    public float velocity;
    public float lastVelocity;
    public float accel;

    // snow sounds
    private float freqMax = 3.0f;
    private float freqMin = 0.5f;

    // wind sounds
    private float pitchMin = 1.0f;
    private float pitchMax = 6.5f;
    private float pitchMult = 0.15f;

    // perlin noise
    float variance = 1.0f;
    float xScale = 1.0f;
    float mult = 0.25f;
    float perlinModifier;

    void Start()
    {
        lastVelocity = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = player.rb.velocity.magnitude;
        accel = (velocity - lastVelocity) / Time.fixedDeltaTime;
        lastVelocity = velocity;
        perlinModifier = variance * Mathf.PerlinNoise(Time.time * xScale, 0.0f);
        updateEnvSounds();
    }

    void updateEnvSounds()
    {
        // snow sounds
        accel = Mathf.Clamp(accel, -5, 0);
        float snowGain = scale(0, -5, freqMin, freqMax, accel);
        boardMixer.SetFloat("snowSound", snowGain);

        // wind sounds
        float windPitch = velocity * pitchMult;
        windPitch = Mathf.Clamp(windPitch, pitchMin, pitchMax);
        windPitch += windPitch * perlinModifier * mult;
        boardMixer.SetFloat("windPitch", windPitch);

    }

    float scale(float a, float b, float c, float d, float oldVal)
    {
        float oldRange = b - a;
        float newRange = d - c;
        float proportion = (oldVal - a) / oldRange;
        float newVal = c + (proportion * newRange);
        return newVal;
    }
}
