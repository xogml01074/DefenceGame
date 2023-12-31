using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public GameObject gameOver;
    public Text scoreTxt;
    public GameObject option;
    public Animator anim;

    public GameManager gameM;

    private void Start()
    {
        rdStart.SetActive(false);
        gameOver.SetActive(false);
    }

    private void Update()
    {
        GameOverUI();
    }

    private void GameOverScore()
    {
        if (gameM.gameOver)
            scoreTxt.text = string.Format($"Round : {gameM.round}\n" +
                                          $"Kill : {gameM.killScore}");
    }

    private void GameOverUI()
    {
        if (gameM.gameOver)
            gameOver.SetActive(true);
        GameOverScore();
    }

    public void OpenOption()
    {
        option.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseOption()
    {
        option.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        option.SetActive(false);
    }
    public void RestartGame()
    {
        gameOver.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Play");
    }

    public void QuitGame()
    {
        gameOver.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
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
            if (gameM.round % 10 != 0)
                roundStartUI.text = $"{round}라운드";
            else
                roundStartUI.text = $"보스 라운드";

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
