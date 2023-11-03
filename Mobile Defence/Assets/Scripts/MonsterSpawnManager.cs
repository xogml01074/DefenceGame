using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterSpawnManager : MonoBehaviour
{
    public Vector3 monsterSpawnPoint1;
    public Vector3 monsterSpawnPoint2;
    public Vector3 monsterSpawnPoint3;
    public Vector3 monsterSpawnPoint4;

    public GameObject monsters;
    public int monsterSpawnCount;
    public float monsterSpawnTime = 1;
    public int monsterLimit;

    public GameManager gameM;

    public void Start()
    {
        monsterLimit = 5;
    }

    public void MonsterSpawn()
    {
        if (!gameM.roundStart && monsterLimit > 0)
            return;

        if (monsterLimit > 0)
            monsterSpawnTime -= Time.deltaTime;

        if (monsterSpawnTime <= 0)
        {
            Instantiate(monsters, monsterSpawnPoint3, Quaternion.identity);
            monsterSpawnTime = 1;
            monsterLimit--;
            monsterSpawnCount++;
        }
    }

    // ���� 40 ���� ���� ü�� �� �̵��ӵ� 
    public void Round40DownMonsterPower()
    {
        if (gameM.round < 10)
        {

        }
        // ���� ������ �⺻, ���ǵ��, ü�¾�, ������ ü�°� ��ȭ

        // 5 ���� ���� ���� ü�°�ȭ + �̵��ӵ� ��ȭ
        // 10 ���� ���� ���� ����Ʈ �߰� ����
    }

    // ���� 40 ���� ���� ü�� �� �̵��ӵ�
    public void Round40UpMonsterPower()
    {
        // �� ���� ���� ���� ü�� ���� �̵��ӵ��� �������� ����
    }

    // ���� ���� ü�� �� �̵��ӵ�
    public void BossMonsterPower()
    {
        // ������ ���������� ��ǥ�� ������ �߰�������Ʈ�� �� ������ �� ����
        // 10���� ���� ���� ���� ��ȯ
        // ���� ���ʹ� ü�¸� ����
    }
}