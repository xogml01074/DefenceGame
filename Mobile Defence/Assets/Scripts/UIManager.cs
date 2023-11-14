using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text remainingTimeUI;
    public Text roundStartUI;
    public GameObject rgTime;
    public GameObject rdStart;

    public GameObject lifeIm;
    public GameObject goldIm;
    public Text lifeUI;
    public Text goldUI;

    public GameManager gameM;

    private void Start()
    {
        rdStart.SetActive(false);
    }

    public void WaitingTimeUI(float remainingTime)
    {
        if (gameM.gameOver)
        {
            rgTime.SetActive(false);
            return;
        }

        if (!gameM.roundStart)
        {
            rdStart.SetActive(false);
            rgTime.SetActive(true);
            remainingTimeUI.text = $"정비 시간: {(int)remainingTime}초";
        }
    }
    public void RoundStartUI(int round)
    {
        if (gameM.gameOver)
        {
            rdStart.SetActive(false);
            return;
        }

        if (gameM.roundStart)
        {
            rgTime.SetActive(false);
            rdStart.SetActive(true);
            roundStartUI.text = $"{round}라운드";
        }
    }

    public void SetPlayerLife(int life)
    {
        if (gameM.gameOver)
        {
            lifeIm.SetActive(false);
            return;
        }

        if (life < 0)
            life = 0;

        lifeUI.text = $"X {life}";
    }

    public void GetGold(int gold)
    {
        if (gameM.gameOver)
        {
            goldIm.SetActive(false);
            return;
        }

        goldUI.text = $"{gold}";
    }
}
