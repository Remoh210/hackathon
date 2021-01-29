using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public MusicManager MusicManager;

    // Start is called before the first frame update
    void Start()
    {
        MusicManager.StartMusic();
    }
}
