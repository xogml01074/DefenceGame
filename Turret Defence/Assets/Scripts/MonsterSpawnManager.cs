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

        if (monsterSpawnTime <= 0)
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

    // 라운드 40 이전 몬스터 체력 및 이동속도 
    public void Round40DownMonsterPower()
    {
        if (gameM.round < 10)
        {

        }
        // 몬스터 종류는 기본, 스피드업, 체력업, 보스는 체력과 둔화

        // 5 라운드 마다 몬스터 체력강화 + 이동속도 강화
        // 10 라운드 마다 스폰 포인트 추가 생성
    }

    // 라운드 40 이후 몬스터 체력 및 이동속도
    public void Round40UpMonsterPower()
    {
        // 매 라운드 마다 몬스터 체력 증가 이동속도는 증가하지 않음
    }

    // 보스 몬스터 체력 및 이동속도
    public void BossMonsterPower()
    {
        // 보스는 여러종류를 목표로 하지만 중간프로젝트엔 한 종류로 할 예정
        // 10라운드 마다 보스 몬스터 소환
        // 보스 몬스터는 체력만 증가
    }
}