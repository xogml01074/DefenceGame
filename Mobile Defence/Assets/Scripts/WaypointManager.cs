using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance;

    public List<Transform> road1;
    public List<Transform> road2;
    public List<Transform> road3;
    public List<Transform> road4; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        road1 = InitializeWaypointsGroup("Road1_Waypoints");
        road2 = InitializeWaypointsGroup("Road2_Waypoints");
        road3 = InitializeWaypointsGroup("Road3_Waypoints");
        road4 = InitializeWaypointsGroup("Road4_Waypoints");
    }

    // ��������Ʈ �׷��� ��������Ʈ���� ����Ʈ�� �߰� (�׷� ������Ʈ�� �ڽ� ��������Ʈ���� ����Ʈ�� �߰�)
    private List<Transform> InitializeWaypointsGroup(string groupName)
    {
        List<Transform> waypointsList = new List<Transform>();
        Transform waypointsGroup = GameObject.Find(groupName).transform; // �׷� ������Ʈ�� Transform ��������

        // �׷� ������Ʈ�� �ڽ� ��������Ʈ���� ����Ʈ�� �߰�
        for (int i = 0; i < waypointsGroup.childCount; i++)
        {
            waypointsList.Add(waypointsGroup.GetChild(i));
        }

        return waypointsList;
    }
}
