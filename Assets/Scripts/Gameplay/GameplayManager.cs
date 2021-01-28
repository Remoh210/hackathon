using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : BeatComponent
{
    public int Countdown { get; set; }

    [SerializeField]
    private MusicManager _musicManager;

    [SerializeField]
    private List<Level> _levels = new List<Level>();

    [SerializeField]
    private int _countdownBeats = 4;

    private Level _currentLevel; 

    void Start()
    {
        Countdown = _countdownBeats;

        //Assume music manager is set
        _musicManager.OnMusicEnded += MusicEnded;

        //Todo: Load level at random? Sequence?
        if(_levels.Count > 0)
        {
            _currentLevel = Instantiate<Level>(_levels[0]);
        }

        _musicManager.StartMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnBeat()
    {
        if(Countdown > 0) 
        {
            Countdown--;
        }
    }

    void MusicEnded()
    {
        Debug.Log("Music ended");
    }
}
