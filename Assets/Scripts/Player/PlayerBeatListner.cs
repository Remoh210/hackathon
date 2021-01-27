using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeatListner : BeatComponent
{
    public override void OnBeat()
    {
        transform.GetComponent<PlayerMovement>().bAllowToMove = true;
    }
}
