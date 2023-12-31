using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class Monsters : MonoBehaviour
{
    List<Transform> road1;
    List<Transform> road2;
    List<Transform> road3;
    List<Transform> road4;

    GameManager gameM;
    PlayerController playerC;
    MonsterSpawnManager monsterSpawnM;
    SoundManager soundM;
    TurretManager turretM;

    Transform endZone;

    GameObject hp;
    Slider hpBar;
    public float maxHp;
    public float currentHp;

    public float monsterSpeed;
    private bool monsterDead;

    private int dstIdx;
    private int spawnPoint;

    private void Start()
    {
        playerC = GameObject.Find("Player").GetComponent<PlayerController>();
        gameM = GameObject.Find("GameManager").GetComponent<GameManager>();
        monsterSpawnM = GameObject.Find("MonsterSpawnManager").GetComponent<MonsterSpawnManager>();
        soundM = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        turretM = GameObject.Find("TurretManager").GetComponent<TurretManager>();

        hp = transform.GetChild(6).gameObject;
        Canvas hpC = hp.GetComponent<Canvas>();
        hpC.worldCamera = Camera.main;
        hpBar = hp.transform.GetChild(0).GetComponent<Slider>();

        playerC.monsters.Add(gameObject); // 생성될 때 PlayerController컴포넌트에 있는 타겟리스트에 추가

        road1 = WaypointManager.Instance.road1;
        road2 = WaypointManager.Instance.road2;
        road3 = WaypointManager.Instance.road3;
        road4 = WaypointManager.Instance.road4;

        GameObject edZone = GameObject.Find("EndZone");
        endZone = edZone.transform;

        SpawnPoint(transform.position);

        monsterSpeed = 1.5f;
        maxHp = gameM.round * 4f;
        currentHp = maxHp;
        monsterDead = false;

        dstIdx = 0;
    }

    private void Update()
    {
        MonsterHpBar();
        MonsterDead();
        MonsterEndZone();
        MonsterMove(GetMonsterRoad());
    }

    private void SpawnPoint(Vector3 sP)
    {
        if (sP == monsterSpawnM.monsterSpawnPoint1)
            spawnPoint = 1;
        else if (sP == monsterSpawnM.monsterSpawnPoint2)
            spawnPoint = 2;
        else if (sP == monsterSpawnM.monsterSpawnPoint3)
            spawnPoint = 3;
        else
            spawnPoint = 4;
    }


    private void MonsterMove(List<Transform> road)
    {
        if (gameM.gameOver)
            return;

        if (!monsterDead)
        {
            // 몬스터가 다음 웨이포인트 까지 도달하지 못했을시 웨이포인트 위치까지 이동
            if (Vector3.Distance(transform.position, road[dstIdx].position) >= 0.05f)
            {
                transform.LookAt(road[dstIdx].position);
                transform.Translate(Vector3.forward * monsterSpeed * Time.deltaTime);
            }
            // 만약 몬스터의 위치가 road1의 다음 웨이포인트에 위치하지 않았다면 몬스터를 다음 웨이포인트로 이동
            else
                dstIdx++;
        }
    }

    private List<Transform> GetMonsterRoad()
    {
        switch (spawnPoint)
        {
            case 1: return road1;
            case 2: return road2;
            case 3: return road3;
            case 4: return road4;

            default : return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GunBullet"))
            GunHurt();

        else if (other.gameObject.CompareTag("CannonBullet"))
            CannonHurt();

        else if (other.gameObject.CompareTag("MissleBullet"))
            MissleHurt();

        else if (other.gameObject.CompareTag("MissleBullet2"))
            Missle2Hurt();
    }

    public void MortorHurt()
    {
        currentHp -= turretM.mortorDamage;
    }

    public void CannonHurt()
    {
        currentHp -= turretM.cannonDamage;
    }

    public void MissleHurt()
    {
        currentHp -= turretM.missleDamage;
    }

    public void Missle2Hurt()
    {
        currentHp -= turretM.missle2Damage;
    }

    public void CatapultHurt()
    {
        currentHp -= turretM.catapultDamage;
    }

    public void GunHurt()
    {
        StartCoroutine(SlowMove());
        currentHp -= playerC.shotDamage;
    }

    IEnumerator SlowMove()
    {
        monsterSpeed = 1f;

        yield return new WaitForSeconds(5f);

        monsterSpeed = 1.5f;
    }

    private void MonsterHpBar()
    {
        hpBar.value = Mathf.Lerp(hpBar.value, currentHp / maxHp, Time.deltaTime * 7);
    }

    // 몬스터가 타워 또는 플레이어에 의해 사망시 실행
    public void MonsterDead()
    {
        if (!monsterDead && currentHp <= 0)
        {
            soundM.MonsterDeadSound();
            gameM.gold++;
            gameM.killScore++;
            gameM.monsterCount--;
            Destroy(gameObject);
        }
    }

    // 몬스터가 목적지 도착시 실행
    public void MonsterEndZone()
    {
        if (!monsterDead)
        {
            if (Vector3.Distance(transform.position, endZone.position) <= 0.1f)
            {
                //soundM.MonsterEndZone(); 추후 몬스터 엔드존 도착 사운드 추가
                gameM.monsterCount--;
                gameM.playerLife--;
                Destroy(gameObject);
            }
        }
    }
}
