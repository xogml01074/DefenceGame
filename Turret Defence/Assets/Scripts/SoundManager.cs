using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource monsterDead;

    public void MonsterDeadSound()
    {
        monsterDead.Play();
    }
}
