using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int playerLife = 5;
    public bool playerLive = true;

    public float remainingTime = 30;
    public int round = 0;
    public bool roundStart = false;

    public int monsterCount;

    public MonsterSpawnManager monsterSM;
    public PlayerController playerC; 
    public UIManager uiM;

    private void Start()
    {
        playerLife = 5;
        remainingTime = 30;
        monsterCount = 0;
    }
    private void Update()
    {
        GameOver();

        uiM.RoundStartUI(round); // 정비시간이 0초가 됐을때 실행
        uiM.WaitingTimeUI(remainingTime); // 정비시간이 0 보다 크면 실행   
        
        RemainingTime();
        RoundStart();

        monsterSM.MonsterSpawn();
    }

    public void RoundStart()
    {
        if (!playerLive)
            return;

        if (remainingTime <= 0)
        {
            roundStart = true;
            if (round < 40)
            {
                if (monsterSM.r1Limit < 25)
                {
                    monsterSM.r1Limit += 4;
                    monsterSM.monsterLimit += 4;
                }

                if (round > 10 && monsterSM.r2Limit < 15)
                {
                    monsterSM.r2Limit += 3;
                    monsterSM.monsterLimit += 3;
                }

                if (round > 20 && monsterSM.r3Limit < 10)
                {
                    monsterSM.r3Limit += 2;
                    monsterSM.monsterLimit += 2;
                }

                if (round > 30 && monsterSM.r2Limit < 10)
                {
                    monsterSM.r4Limit += 2;
                    monsterSM.monsterLimit += 2;
                }
            }

            monsterCount = monsterSM.monsterLimit;
            round++;
            remainingTime = 20;

            if (!(round % 10 == 0) && round < 40)
            {
                // monsterSM.Round40DownMonsterPower()
            }
            else if (!(round % 10 == 0) && round > 40)
            {
                // monsterSM.Round40UPMonsterPower()
            }
        }
    }

    // 정비시간 매소드
    public void RemainingTime()
    {
        if (!playerLive)
            return;

        if (remainingTime > 0 && monsterCount == 0)
        {
            monsterSM.currentLimit = 0;
            playerC.monsters.Clear();
            remainingTime -= Time.deltaTime;
            roundStart = false;
        }
    }

    public void GameOver()
    {
        if (!playerLive)
            return;

        if (playerLife == 0)
        {
            Debug.Log("Game Over"); // 게임오버시 나오는 재시작 또는 메뉴화면으로 이동 UI
            roundStart = false;
            playerLive = false;
        }
    }
}