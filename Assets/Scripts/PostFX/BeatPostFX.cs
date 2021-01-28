using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BeatPostFX : BeatComponent
{
    public float ChromaMaxIntensity = 0.7f;
    public float ChromaAmpSpeed = 0.2f;
    

    private Volume PostFXVolume;

    private float InitChromaIntensity;
    private ChromaticAberration Chroma;
    public void Start()
    {
        PostFXVolume = gameObject.GetComponent<Volume>();
        PostFXVolume.profile.TryGet(out Chroma);
        InitChromaIntensity = Chroma.intensity.value;
    }
    public override void OnBeat()
    {
        StartCoroutine(PingPongFX());
    }

    private IEnumerator PingPongFX()
    {
        for (float time = 0; time < ChromaAmpSpeed * 2; time += Time.deltaTime)
        {
            float progress = Mathf.PingPong(time, ChromaAmpSpeed) / ChromaAmpSpeed;
            Chroma.intensity.value = Mathf.Lerp(InitChromaIntensity, ChromaMaxIntensity, progress);
            yield return null;
        }
        Chroma.intensity.value = InitChromaIntensity;
    }
}
