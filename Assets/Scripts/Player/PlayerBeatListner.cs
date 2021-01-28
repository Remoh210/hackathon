using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeatListner : BeatComponent
{
    [SerializeField]
    Animator AnimationController;

    public override void OnBeat()
    {
        transform.GetComponent<PlayerMovement>().bAllowToMove = true;
        AnimationController.SetTrigger("BeatTrigger");
    }

    public override void OnBeatStarted()
    {
    }
}
