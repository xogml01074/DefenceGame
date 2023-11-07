using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class TurretCreateManager : MonoBehaviour
{
    public GameObject cannon;
    public GameObject catapult;
    public GameObject missle;
    public GameObject missle2;
    public GameObject mortor;

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
                    Debug.Log("타워종류 설정 UI"); // 타워종류 UI 만든 후 나오게 만들고 클릭시 그 타워 생성
            }
        }
    }

    // 터렛 생성 매소드
    private void TurretCreat()
    {

    }
}