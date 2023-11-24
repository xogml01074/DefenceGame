using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterSpawnManager : MonoBehaviour
{
    public Vector3 monsterSpawnPoint1;
    public Vector3 monsterSpawnPoint2;
    public Vector3 monsterSpawnPoint3;
    public Vector3 monsterSpawnPoint4;

    public GameObject boss;
    public bool bossLive = false;
    public Transform[] bossSpawnPs;
    public GameObject monsters;
    public float monsterSpawnTime = 1;
    public int r1Limit = 5;
    public int r2Limit = 0;
    public int r3Limit = 0;
    public int r4Limit = 0;
    public int monsterLimit;

    public GameManager gameM;

    private void Update()
    {
        MonsterSpawn();
    }

    public void MonsterCount()
    {
        if (gameM.round == 0)
        {
            monsterLimit = 5;
            return;
        }

        if (gameM.round < 40)
        {
            r1Limit = 5 + gameM.round % 10 * 4;
            
            if (gameM.round >= 10)
            r2Limit = 3 + gameM.round % 10 * 3;

            if (gameM.round >= 20)
            r3Limit = 3 + gameM.round % 10 * 3;

            if (gameM.round >= 30)
            r4Limit = 2 + gameM.round % 10 * 3;
        }
        else if (gameM.round >= 40)
        {
            r1Limit = 50;
            r2Limit = 40;
            r3Limit = 30;
            r4Limit = 20;
        }
        monsterLimit = r1Limit + r2Limit + r3Limit + r4Limit;
    }
    public void MonsterSpawn()
    {
        if (!gameM.roundStart || gameM.gameOver)
            return;

        if (monsterLimit > 0)
            monsterSpawnTime -= Time.deltaTime;

        if (monsterSpawnTime <= 0 && gameM.round % 10 != 0)
        {
            if (r1Limit > 0)
            {
                Instantiate(monsters, monsterSpawnPoint1, Quaternion.identity);
                r1Limit--;
            }

            if (r2Limit > 0)
            {
                Instantiate(monsters, monsterSpawnPoint2, Quaternion.identity);
                r2Limit--;
            }

            if (r3Limit > 0)
            {
                Instantiate(monsters, monsterSpawnPoint3, Quaternion.identity);
                r3Limit--;
            }

            if (r4Limit > 0)
            {
                Instantiate(monsters, monsterSpawnPoint4, Quaternion.identity);
                r4Limit--;
            }

            monsterSpawnTime = 1;
        }
    }

    public void BossMonsterSpawn()
    {
        // 보스는 여러종류를 목표로 하지만 중간프로젝트엔 한 종류로 할 예정
        // 10라운드 마다 보스 몬스터 소환
        int rdIdx = Random.Range(0, 3);
        Instantiate(boss, bossSpawnPs[rdIdx].position, Quaternion.identity);
    }
}