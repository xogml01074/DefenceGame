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

        uiM.RoundStartUI(round); // ����ð��� 0�ʰ� ������ ����
        uiM.WaitingTimeUI(remainingTime); // ����ð��� 0 ���� ũ�� ����   
        
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
            if (monsterSM.monsterLimit < 50)
                monsterSM.monsterLimit += 5;

            monsterCount = monsterSM.monsterLimit;
            round++;
            remainingTime = 20;

            if (!(round % 10 == 0) && round < 40)
            {
                // monsterSM.Round40DownMonsterPower()
            }
            else if (!(round % 10 == 0) && round > 40)
            {
                monsterSM.monsterLimit = 50;
                // monsterSM.Round40UPMonsterPower()
            }
        }
    }

    // ����ð� �żҵ�
    public void RemainingTime()
    {
        if (!playerLive)
            return;

        if (remainingTime > 0 && monsterCount == 0)
        {
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
            Debug.Log("Game Over"); // ���ӿ����� ������ ����� �Ǵ� �޴�ȭ������ �̵� UI
            roundStart = false;
            playerLive = false;
        }
    }
}