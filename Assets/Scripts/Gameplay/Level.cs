using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Level : MonoBehaviour
{
    public Action<int> OnRoundWon;

    [SerializeField]
    PlayerManager[] _teams;

    // Start is called before the first frame update
    void Awake()
    {
        if(_teams.Length != 2)
        {
            Debug.LogError("Incorrect teams assigned to level prefab");
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool t0 = _teams[0].CheckAlive();
        bool t1 = _teams[1].CheckAlive();
        bool both = !t0 && !t1;

        if(!t0 || !t1)
        {
            OnRoundWon?.Invoke(both ? -1 : !t0 ? 1 : 0 );
        }
    }
}
