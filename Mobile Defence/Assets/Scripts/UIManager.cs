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

    public GameManager gameM;

    private void Start()
    {
        rdStart.SetActive(false);
    }

    public void WaitingTimeUI(float remainingTime)
    {
        if (!gameM.roundStart)
        {
            rdStart.SetActive(false);
            rgTime.SetActive(true);
            remainingTimeUI.text = $"정비 시간: {(int)remainingTime}초";
        }
    }
    public void RoundStartUI(int round)
    {
        if (gameM.roundStart)
        {
            rgTime.SetActive(false);
            rdStart.SetActive(true);
            roundStartUI.text = $"{round}라운드";
        }
    }
}
