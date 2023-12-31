using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using static TowerSpawnTorF;

public class TurretManager : MonoBehaviour
{
    public TurretUi tU;
    public GameManager gameM;
    public GameObject canvas;
    public Animator anim;

    public GameObject effect;

    public GameObject cannon;
    public GameObject catapult;
    public GameObject missle1;
    public GameObject missle2;
    public GameObject mortor;

    public float cannonDamage = 15;
    public float missleDamage = 20;
    public float missle2Damage = 12;
    public float catapultDamage = 30;
    public float mortorDamage = 50;

    public Vector3 spawnP;
    Vector3 mousePos;
    GameObject hg;

    private void Update()
    {
        if (gameM.gameOver)
            return;

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
                    hg = hit.transform.gameObject;
                    if (hg.GetComponent<TowerSpawnTorF>().ts == TowerSpawnState.tsTrue)
                    {                       
                        if (spawnP == new Vector3(0, 0, 0))
                            spawnP = hit.collider.transform.position;

                        canvas.SetActive(true); // 타워종류 UI 만든 후 나오게 만들고 클릭시 그 타워 생성
                    }
                }
            }
        }
    }

    public void CannonCreate()
    {
        if (gameM.gold < 15)
            return;

        if (gameM.gold >= 15)
        {
            gameM.gold -= 15;
            Destroy(Instantiate(effect, spawnP + new Vector3(-1, 1.5f, 1), Quaternion.identity), 3f);
            Instantiate(cannon, spawnP + new Vector3(-1, 1, 1), Quaternion.identity);
            tU.ExitClickButton();
        }
        hg.GetComponent<TowerSpawnTorF>().ts = TowerSpawnState.tsFalse;
    }
    public void Missle1Create()
    {
        if (gameM.gold < 40)
            return;

        if (gameM.gold >= 40)
        {
            gameM.gold -= 40;
            Destroy(Instantiate(effect, spawnP + new Vector3(-1, 1.5f, 1), Quaternion.identity), 3f);
            Instantiate(missle1, spawnP + new Vector3(-1, 1, 1), Quaternion.identity);
            tU.ExitClickButton();
        }
        hg.GetComponent<TowerSpawnTorF>().ts = TowerSpawnState.tsFalse;
    }
    public void Missle2Create()
    {
        if (gameM.gold < 90)
            return;

        if (gameM.gold >= 90)
        {
            gameM.gold -= 90;
            Destroy(Instantiate(effect, spawnP + new Vector3(-1, 1.5f, 1), Quaternion.identity), 3f);
            Instantiate(missle2, spawnP + new Vector3(-1, 1, 1), Quaternion.identity);
            tU.ExitClickButton();
        }
        hg.GetComponent<TowerSpawnTorF>().ts = TowerSpawnState.tsFalse;
    }
    public void CatapultCreate()
    {
        if (gameM.gold < 150)
            return;

        if (gameM.gold >= 150)
        {
            gameM.gold -= 150;
            Destroy(Instantiate(effect, spawnP + new Vector3(-1, 1.5f, 1), Quaternion.identity), 3f);
            Instantiate(catapult, spawnP + new Vector3(-1, 1, 1), Quaternion.identity);
            tU.ExitClickButton();
            hg.GetComponent<TowerSpawnTorF>().ts = TowerSpawnState.tsFalse;
        }
    }
    public void MortorCreate()
    {
        if (gameM.gold < 200)
            return;

        if (gameM.gold >= 200)
        {
            gameM.gold -= 200;
            Destroy(Instantiate(effect, spawnP + new Vector3(-1, 1.5f, 1), Quaternion.identity), 3f);
            Instantiate(mortor, spawnP + new Vector3(-1, 1, 1), Quaternion.identity);
            tU.ExitClickButton();
            hg.GetComponent<TowerSpawnTorF>().ts = TowerSpawnState.tsFalse;
        }
    }
}
