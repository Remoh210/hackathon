using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private float BeatPerMinute = 100.0f;

    private AudioSource AudioData;

    private float BeatInterval = 0.0f;

    private double StartTime;
    private double NextBeatTime;

    private BeatComponent[] BeatComponents;

    void Start()
    {
        BeatComponents = GameObject.FindObjectsOfType<BeatComponent>();
        AudioData = GetComponent<AudioSource>();

        BeatInterval = 60.0f / BeatPerMinute;

        //Get the audio time from the audio system, this is apparently a better way of doing this
        StartTime = AudioSettings.dspTime;
        NextBeatTime = StartTime + BeatInterval;

        AudioData.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (AudioData.isPlaying)
        {
            UpdateBeat();
        }
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 70, 150, 30), "Pause"))
        {
            AudioData.Pause();
            Debug.Log("Pause: " + AudioSettings.dspTime);
        }

        if (GUI.Button(new Rect(10, 170, 150, 30), "Continue"))
        {
            AudioData.UnPause();
        }
    }

    private void UpdateBeat()
    {
        if (AudioSettings.dspTime > NextBeatTime)
        {
            NextBeatTime += BeatInterval;

            foreach (BeatComponent comp in BeatComponents)
            {
                comp.OnBeat();
            }
        }
    }
}
