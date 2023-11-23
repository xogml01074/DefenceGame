using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterSpawnManager : MonoBehaviour
{
    public Vector3 monsterSpawnPoint1;
    public Vector3 monsterSpawnPoint2;
    public Vector3 monsterSpawnPoint3;
    public Vector3 monsterSpawnPoint4;

    public GameObject boss;
    public int bossCount = 1;
    public bool bossLive = false;
    public Transform[] bossSpawnPs;
    public GameObject monsters;
    public int monsterLimit = 5;
    public int currentLimit;
    public float monsterSpawnTime = 1;
    public int r1Limit = 5;
    public int r2Limit = 0;
    public int r3Limit = 0;
    public int r4Limit = 0;

    public GameManager gameM;

    public void MonsterSpawn()
    {
        if (!gameM.roundStart && monsterLimit > 0 || gameM.gameOver)
            return;

        if (currentLimit < monsterLimit)
            monsterSpawnTime -= Time.deltaTime;

        if (monsterSpawnTime <= 0 && gameM.round % 10 != 0)
        {
            if (r1Limit > 0)
            {
                Instantiate(monsters, monsterSpawnPoint1, Quaternion.identity);
                currentLimit++;
            }

            if (r2Limit > 0)
            {
                Instantiate(monsters, monsterSpawnPoint2, Quaternion.identity);
                currentLimit++;
            }

            if (r3Limit > 0)
            {
                Instantiate(monsters, monsterSpawnPoint3, Quaternion.identity);
                currentLimit++;
            }

            if (r4Limit > 0)
            {
                Instantiate(monsters, monsterSpawnPoint4, Quaternion.identity);
                currentLimit++;
            }

            monsterSpawnTime = 1;
        }
    }

    public void BossMonsterSpawn()
    {
        // 보스는 여러종류를 목표로 하지만 중간프로젝트엔 한 종류로 할 예정
        // 10라운드 마다 보스 몬스터 소환
        if (gameM.roundStart && gameM.round % 10 == 0 && bossCount == 1)
        {
            bossCount--;
            int rdIdx = Random.Range(0, 3);
            Instantiate(boss, bossSpawnPs[rdIdx].position, Quaternion.identity);
        }
    }
}