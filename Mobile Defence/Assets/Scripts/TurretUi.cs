using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUi : MonoBehaviour
{
    public Animator anim;
    public GameObject canvas;

    public void ExitClickButton()
    {
        anim.SetTrigger("ClickExit");
    }

    // 애니메이션 이벤트로 실행
    public void ClickButton()
    {
        canvas.SetActive(false);
    }
}
