using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource AudioData;
    public float UpdateStep;

    private float ClipLoudness;
    private float[] ClipSampleData;
    private int SampleDataLength = 1024;

    public float BeatPerMinute = 100.0f;
    private float BeatInterval = 0.0f;

    private float CurTime;

    private BeatComponent[] BeatComponents;

    private void Awake()
    {
        ClipSampleData = new float[SampleDataLength];
    }

    void Start()
    {
        AudioData = GetComponent<AudioSource>();
        AudioData.Play(0);

        BeatInterval = 60.0f / BeatPerMinute;

        BeatComponents = GameObject.FindObjectsOfType<BeatComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!AudioData.isPlaying)
        {
            return;
        }

        UpdatePlayersMovement();
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 70, 150, 30), "Pause"))
        {
            AudioData.Pause();
            Debug.Log("Pause: " + AudioData.time);
        }

        if (GUI.Button(new Rect(10, 170, 150, 30), "Continue"))
        {
            AudioData.UnPause();
        }
    }

    private void UpdatePlayersMovement()
    {
        CurTime += Time.deltaTime;
        if (CurTime > BeatInterval)
        {
            CurTime = 0.0f;
        
            foreach (BeatComponent comp in BeatComponents)
            {
                comp.OnBeat();
            }
        }
    }
}
