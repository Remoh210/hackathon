using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public int[] _scores = new int[2];

    void Start()
    {
        Countdown = _countdownBeats;

        //Assume music manager is set
        _musicManager.OnMusicEnded += MusicEnded;

        SpawnLevel();

        _musicManager.StartMusic();
    }

    void SpawnLevel()
    {
        if(_currentLevel != null)
        {
            Destroy(_currentLevel.gameObject);
            _currentLevel = null;
        }

        //Todo: Load level at random? Sequence?
        if(_levels.Count > 0)
        {
            _currentLevel = Instantiate<Level>(_levels[0]);
            _currentLevel.OnRoundWon += RoundWon;
        }

        StartCoroutine(RefreshMusicManager());
    }
    IEnumerator RefreshMusicManager()
    {
        //Hack to work around destroyed objects. Not good...
        yield return new WaitForEndOfFrame();
        _musicManager.RecacheBeatComponents();
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

    public int GetScore(int team)
    {
        return _scores[team];
    }

    void MusicEnded()
    {
        Debug.Log("Music ended");
        SceneManager.LoadScene(0);
    }

    void RoundWon(int team)
    {
        if(team >= 0)
        {
            _scores[team]++;
            SpawnLevel();
        }
    }
}
