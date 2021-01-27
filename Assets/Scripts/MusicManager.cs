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

    public GameObject UIToScale;

    public float SizeFactor = 1;
    public float MinSize = 0.0f;
    public float MaxSize = 10.0f;

    public float BeatPerMinute = 100.0f;
    private float BeatInterval = 0.0f;

    public float HitTolerance = 0.2f;
    private float CurTime;

    private PlayerMovement[] MovementComponents;

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
        MovementComponents = GameObject.FindObjectsOfType<PlayerMovement>();

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
            foreach (PlayerMovement comp in MovementComponents)
            {
                comp.bAllowToMove = true;
            }
            CurTime = 0.0f;


            foreach (BeatComponent comp in BeatComponents)
            {
                comp.OnBeat();
            }
        }
    }
}
