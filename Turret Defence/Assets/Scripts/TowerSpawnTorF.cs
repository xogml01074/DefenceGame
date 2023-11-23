using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawnTorF : MonoBehaviour
{
    public enum TowerSpawnState
    {
        tsTrue,
        tsFalse,
    }

    public TowerSpawnState ts;

    private void Start()
    {
        ts = TowerSpawnState.tsTrue;
    }
}
