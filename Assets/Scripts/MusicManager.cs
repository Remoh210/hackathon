using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public Action OnMusicEnded;

    public int BeatPerMinute = 120;

    public float BeatInterval { get; set; } = 0.0f;

    private AudioSource AudioData;
    private double StartTime;
    private double NextBeatTime;

    private BeatComponent[] BeatComponents;

    void Awake()
    {
        //Debug.Log("Starting audio");
        AudioData = GetComponent<AudioSource>();
        BeatInterval = 60.0f / (float)BeatPerMinute;
    }

    public void StartMusic()
    {
        RecacheBeatComponents();

        //Get the audio time from the audio system, this is apparently a better way of doing this
        StartTime = AudioSettings.dspTime;
        NextBeatTime = StartTime + BeatInterval;

        AudioData.Play(0);

        foreach (BeatComponent comp in BeatComponents)
        {
            comp?.OnBeatStarted();
        }
    }

    public void RecacheBeatComponents()
    {
        Debug.Log("Recaching beat components");
        BeatComponents = GameObject.FindObjectsOfType<BeatComponent>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (AudioData.isPlaying)
        {
            UpdateBeat();
        }
        else if(AudioData.time >= AudioData.clip.length)
        {
            OnMusicEnded?.Invoke();
        }
    }

/*
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
*/
    private void UpdateBeat()
    {
        if (AudioSettings.dspTime > NextBeatTime)
        {
            NextBeatTime += BeatInterval;

            foreach (BeatComponent comp in BeatComponents)
            {
                comp?.OnBeat();
            }
        }
    }
}
