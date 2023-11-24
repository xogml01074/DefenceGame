using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Animator htpImageAni;
    public Animator htpAni;
    public Animator bestScoreAni;

    public Text bestScroe;
    private int lastRound = 0;
    private int lastKill = 0;
    private int bestRound = 0;
    private int bestKill = 0;

    private void Awake()
    {
        SetBestScore();
    }

    private void SetBestScore()
    {
        lastRound = PlayerPrefs.GetInt("TotalRound", 0);
        lastKill = PlayerPrefs.GetInt("TotalKill", 0);

        if (lastRound > bestRound)
        {
            PlayerPrefs.SetInt("BestRound", lastRound);
            bestRound = PlayerPrefs.GetInt("BestRound", 0);
        }

        if (lastKill > bestKill)
        {
            PlayerPrefs.SetInt("BestKill", lastKill);
            bestKill = PlayerPrefs.GetInt("BestKill", 0);
        }
        PlayerPrefs.Save();


        bestScroe.text = string.Format($" Round : {bestRound}\n" +
                                       $" Kill : {bestKill}");
    }

    public void BestScoreButton()
    {
        bestScoreAni.SetBool("clickBtn", true);
    }

    public void BestScoreCloseButton()
    {
        bestScoreAni.SetBool("clickBtn", false);
    }

    public void StartButton()
    {
        Invoke(nameof(LoadPlayScene), 1f);
    }

    private void LoadPlayScene()
    {
        SceneManager.LoadScene("Play");
    }

    public void HowToPlayButton()
    {
        htpImageAni.SetBool("HowToPlayBtn", true);
    }

    public void HowToPlayClose()
    {
        htpImageAni.SetBool("HowToPlayBtn", false);
    }

    public void HTP1PageSlideRight()
    {
        htpAni.SetBool("1p~2p", true);
    }

    public void HTP2PageSlideLeft()
    {
        htpAni.SetBool("1p~2p", false);
    }

    public void HTP2PageSlideRight()
    {
        htpAni.SetBool("2p~3p", true);
    }

    public void HTP3PageSlideLeft()
    {
        htpAni.SetBool("2p~3p", false);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
