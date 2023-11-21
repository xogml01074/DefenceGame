using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossMonsterAI : MonoBehaviour
{
    private PlayerController playerC;
    private GameManager gameM;
    private TurretManager turretM;
    private MonsterSpawnManager monsterSM;

    private NavMeshAgent agent;
    private GameObject target;

    public Slider hpBar;
    public float maxHp;
    public float currentHp;


    private void Start()
    {
        monsterSM = GameObject.Find("MonsterSpawnManager").GetComponent<MonsterSpawnManager>();
        monsterSM.bossLive = true;

        target = GameObject.Find("Player");
        playerC = target.GetComponent<PlayerController>();
        playerC.monsters.Add(gameObject);

        turretM = GameObject.Find("TurretManager").GetComponent<TurretManager>();
        gameM = GameObject.Find("GameManager").GetComponent<GameManager>();

        agent = GetComponent<NavMeshAgent>();

        maxHp = gameM.round * 20;
        currentHp = maxHp;
    }

    private void Update()
    {
        BossDead();
        FindTarget();
        SetBossHpBar();
    }

    private void FindTarget()
    {
        if (!monsterSM.bossLive)
            return;

        agent.SetDestination(target.transform.position);
    }

    private void SetBossHpBar()
    {
        hpBar.value = Mathf.Lerp(hpBar.value, currentHp / maxHp, Time.deltaTime * 7);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GunBullet"))
            currentHp -= playerC.shotDamage * 1.5f;

        else if (other.gameObject.CompareTag("MissleBullet"))
            MissleHurt();

        else if (other.gameObject.CompareTag("MissleBullet2"))
            Missle2Hurt();

        else if (other.gameObject.CompareTag("CannonBullet"))
            CannonHurt();
    }

    public void MortorHurt()
    {
        currentHp -= turretM.mortorDamage / 4;
    }

    public void CannonHurt()
    {
        currentHp -= turretM.cannonDamage;
    }

    public void MissleHurt()
    {
        currentHp -= turretM.missleDamage / 2;
    }

    public void Missle2Hurt()
    {
        currentHp -= turretM.missle2Damage / 2;
    }

    public void CatapultHurt()
    {
        currentHp -= turretM.catapultDamage / 4;
    }

    private void BossDead()
    {
        if(currentHp <= 0)
        {
            gameM.gold += 100;
            monsterSM.bossCount = 1;
            monsterSM.bossLive = false;
            Destroy(gameObject);
        }
    }
}
