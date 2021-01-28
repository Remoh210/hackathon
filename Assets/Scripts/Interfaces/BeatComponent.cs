using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class for implementing OnBeat logic
public class BeatComponent : MonoBehaviour
{
    //Called Every beat
    public virtual void OnBeat() { }

    public virtual void OnBeatStarted() {}
}
