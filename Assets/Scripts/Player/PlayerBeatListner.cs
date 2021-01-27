using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeatListner : BeatComponent
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnBeat()
    {
        transform.GetComponent<PlayerMovement>().bAllowToMove = true;
        Debug.Log("TEST");
    }
}
