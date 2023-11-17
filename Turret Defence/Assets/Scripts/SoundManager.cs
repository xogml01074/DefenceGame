using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource monsterDead;
    public AudioSource gunShot;
    

    public void GunShotSound()
    {
        gunShot.Play();
    }
    public void MonsterDeadSound()
    {
        monsterDead.Play();
    }
}
