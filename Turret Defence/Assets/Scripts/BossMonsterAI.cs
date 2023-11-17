using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossMonsterAI : MonoBehaviour
{
    private PlayerController playerC;
    private GameManager gameM;

    private NavMeshAgent agent;
    private GameObject target;

    public Slider hpBar;
    public float maxHp;
    public float currentHp;
    private bool bossDead = false;

    private void Start()
    {
        target = GameObject.Find("Player");
        playerC = target.GetComponent<PlayerController>();
        playerC.monsters.Add(gameObject);
        
        gameM = GameObject.Find("GameManager").GetComponent<GameManager>();

        agent = GetComponent<NavMeshAgent>();

        maxHp = gameM.round * 20;
        currentHp = maxHp;
    }

    private void Update()
    {
        FindTarget();
        SetBossHpBar();
    }

    private void FindTarget()
    {
        agent.SetDestination(target.transform.position);
    }

    private void SetBossHpBar()
    {
        hpBar.value = Mathf.Lerp(hpBar.value, currentHp / maxHp, Time.deltaTime * 7);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == CompareTag("Bullet"))
            currentHp -= playerC.shotDamage;
    }
}
