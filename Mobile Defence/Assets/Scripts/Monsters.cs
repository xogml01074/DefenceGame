using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Monsters : MonoBehaviour
{
    List<Transform> road1;
    List<Transform> road2;
    List<Transform> road3;
    List<Transform> road4;

    GameManager gameM;
    PlayerController playerC;
    MonsterSpawnManager monsterSpawnM;

    Transform endZone;

    public float monsterSpeed;
    private bool monsterDead;
    public float monsterLife;

    private int dstIdx;
    private int spawnPoint;

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

    private void Start()
    {
        playerC = GameObject.Find("Player").GetComponent<PlayerController>();
        gameM = GameObject.Find("GameManager").GetComponent<GameManager>();
        monsterSpawnM = GameObject.Find("MonsterSpawnManager").GetComponent<MonsterSpawnManager>();

        playerC.monsters.Add(transform); // ������ �� PlayerController������Ʈ�� �ִ� Ÿ�ٸ���Ʈ�� �߰�

        road1 = WaypointManager.Instance.road1;
        road2 = WaypointManager.Instance.road2;
        road3 = WaypointManager.Instance.road3;
        road4 = WaypointManager.Instance.road4;

        GameObject edZone = GameObject.Find("EndZone");
        endZone = edZone.transform;

        SpawnPoint(transform.position);

        monsterSpeed = 3;
        monsterLife = gameM.round * 5;
        monsterDead = false;

        dstIdx = 0;
    }

    private void Update()
    {
        MonsterEndZone();
        MonsterDead();
        MonsterMove(GetMonsterRoad());

    }

    private void MonsterMove(List<Transform> road)
    {
        if (!monsterDead)
        {
            // ���Ͱ� ���� ��������Ʈ ���� �������� �������� ��������Ʈ ��ġ���� �̵�
            if (Vector3.Distance(transform.position, road[dstIdx].position) >= 0.01f)
            {
                transform.LookAt(road[dstIdx].position);
                transform.Translate(Vector3.forward * monsterSpeed * Time.deltaTime);
            }
            // ���� ������ ��ġ�� road1�� ���� ��������Ʈ�� ��ġ���� �ʾҴٸ� ���͸� ���� ��������Ʈ�� �̵�
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

    // ���Ͱ� Ÿ�� �Ǵ� �÷��̾ ���� ����� ����
    public void MonsterDead()
    {
        if (!monsterDead && monsterLife <= 0)
        {
            gameM.monsterCount--;
            playerC.monsters.RemoveAt(0);
            Destroy(gameObject);
        }
    }

    // ���Ͱ� ������ ������ ����
    public void MonsterEndZone()
    {
        if (!monsterDead)
        {
            if (Vector3.Distance(transform.position, endZone.position) <= 0.01f)
            {
                gameM.monsterCount--;
                gameM.playerLife--;
                playerC.monsters.RemoveAt(0);
                Destroy(gameObject);
            }
        }
    }
}
