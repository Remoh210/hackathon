using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource AudioData;

    public float BeatPerMinute = 100.0f;
    private float BeatInterval = 0.0f;

    private float CurentTime;

    private BeatComponent[] BeatComponents;

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
            Debug.Log("Pause: " + AudioData.time);
        }

        if (GUI.Button(new Rect(10, 170, 150, 30), "Continue"))
        {
            AudioData.UnPause();
        }
    }

    private void UpdateBeat()
    {
        CurentTime += Time.deltaTime;
        if (CurentTime > BeatInterval)
        {
            CurentTime = 0.0f;
            foreach (BeatComponent comp in BeatComponents)
            {
                comp.OnBeat();
            }
        }
    }
}
