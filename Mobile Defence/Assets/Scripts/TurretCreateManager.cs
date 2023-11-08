using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class TurretCreateManager : MonoBehaviour
{
    public GameObject canvas;
    public Animator anim;

    public GameObject cannon;
    public GameObject catapult;
    public GameObject missle;
    public GameObject missle2;
    public GameObject mortor;

    public Vector3 spawnP;
    Vector3 mousePos;

    private void Update()
    {
        TurretCreatePointClick();
    }

    private void TurretCreatePointClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == "Tile_StoneV1")
                {
                    canvas.SetActive(true); // 타워종류 UI 만든 후 나오게 만들고 클릭시 그 타워 생성
                    spawnP = hit.collider.transform.position;
                }
            }
        }
    }

    // 터렛 생성 매소드
    private void TurretCreat()
    {

    }
}
