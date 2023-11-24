using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int playerLife = 5;
    public bool playerLive = true;

    public float remainingTime = 30.5f;
    public int round = 0;
    public bool roundStart = false;
    public bool gameOver = false;

    public int killScore = 0;

    public int gold;

    public int monsterCount;

    public MonsterSpawnManager monsterSM;
    public PlayerController playerC; 
    public UIManager uiM;

    private void Start()
    {
        gold = 0;

        playerLife = 5;
        remainingTime = 10.9f;
        monsterCount = 0;
    }
    private void Update()
    {
        RoundStart();
        GameOver();
        RemainingTime();

        uiM.RoundStartUI(round); // 정비시간이 0초가 됐을때 실행
        uiM.WaitingTimeUI(remainingTime); // 정비시간이 0 보다 크면 실행   
        uiM.SetPlayerLife(playerLife);
        uiM.GetGold(gold);        
    }

    public void RoundStart()
    {
        if (!playerLive)
            return;

        if (remainingTime <= 0)
        {
            monsterCount = monsterSM.monsterLimit;

            round++;
            if (round != 0 && round % 10 == 0)
                monsterSM.BossMonsterSpawn();

            roundStart = true;
            remainingTime = 15.9f;


            playerC.shotDamage += 0.5f;
        }
    }

    // 정비시간 매소드
    public void RemainingTime()
    {
        if (!playerLive)
            return;

        if (monsterCount == 0)
        {
            monsterSM.MonsterCount();

            playerC.monsters.Clear();
            remainingTime -= Time.deltaTime;
            roundStart = false;
        }
    }

    public void GameOver()
    {
        if (!playerLive)
            return;

        if (playerLife <= 0)
        {
            gameOver = true;
            PlayerPrefs.SetInt("TotalRound", round);
            PlayerPrefs.SetInt("TotalKill", killScore);
            PlayerPrefs.Save();
            roundStart = false;
            playerLive = false;
        }
    }
}