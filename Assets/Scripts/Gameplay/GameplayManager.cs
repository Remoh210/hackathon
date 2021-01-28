using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private MusicManager _musicManager;

    [SerializeField]
    private List<Level> _levels;

    private Level _currentLevel; 

    void Start()
    {
        //Assume music manager is set
        _musicManager.OnMusicEnded += MusicEnded;

        //Todo: Load level at random? Sequence?
        _currentLevel = Instantiate<Level>(_levels[0]);

        _musicManager.StartMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MusicEnded()
    {
        Debug.Log("Music ended");
    }
}
