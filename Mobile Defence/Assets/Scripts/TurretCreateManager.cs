using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class TurretCreateManager : MonoBehaviour
{
    public TurretUi tU;
    public GameObject canvas;
    public Animator anim;

    public GameObject effect;

    public GameObject cannon;
    public GameObject catapult;
    public GameObject missle1;
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
                    if (spawnP == new Vector3(0, 0, 0))
                        spawnP = hit.collider.transform.position;                        

                    canvas.SetActive(true); // 타워종류 UI 만든 후 나오게 만들고 클릭시 그 타워 생성
                }
            }
        }
    }

    public void CannonCreate()
    {
        Destroy(Instantiate(effect, spawnP + new Vector3(-1, 1.5f, 1), Quaternion.identity), 3f);
        Instantiate(cannon, spawnP + new Vector3 (-1, 1, 1), Quaternion.identity);
        tU.ExitClickButton();
    }
    public void Missle1Create()
    {
        Destroy(Instantiate(effect, spawnP + new Vector3(-1, 1.5f, 1), Quaternion.identity), 3f);
        Instantiate(missle1, spawnP + new Vector3(-1, 1, 1), Quaternion.identity);
        tU.ExitClickButton();
    }
    public void Missle2Create()
    {
        Destroy(Instantiate(effect, spawnP + new Vector3(-1, 1.5f, 1), Quaternion.identity), 3f);
        Instantiate(missle2, spawnP + new Vector3(-1, 1, 1), Quaternion.identity);
        tU.ExitClickButton();
    }
    public void CatapultCreate()
    {
        Destroy(Instantiate(effect, spawnP + new Vector3(-1, 1.5f, 1), Quaternion.identity), 3f);
        Instantiate(catapult, spawnP + new Vector3(-1, 1, 1), Quaternion.identity);
        tU.ExitClickButton();
    }
    public void MortorCreate()
    {
        Destroy(Instantiate(effect, spawnP + new Vector3(-1, 1.5f, 1), Quaternion.identity), 3f);
        Instantiate(mortor, spawnP + new Vector3(-1, 1, 1), Quaternion.identity);
        tU.ExitClickButton();
    }
}
