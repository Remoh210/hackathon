using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeatListner : BeatComponent
{
    [SerializeField]
    Animator AnimationController;

    public override void OnBeat()
    {
        PlayerMovement Movement = GetComponent<PlayerMovement>();
        if(Movement)
        {
            transform.GetComponent<PlayerMovement>().bAllowToMove = true;
        }
        //Debug.Log("Beat");
        AnimationController.SetTrigger("BeatTrigger");
    }

    public override void OnBeatStarted()
    {
    }
}
