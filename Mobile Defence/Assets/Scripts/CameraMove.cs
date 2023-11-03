using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target; // ī�޶� ���� ���
    public Vector3 distance = new Vector3(0, 15, -4); // ī�޶�� �ɸ��� ������ �Ÿ�
    public Vector3 rotation = new Vector3(70, 0, 0); // ī�޶��� ����

    private void LateUpdate()
    {
        if(target != null)
        {
            // ĳ������ ��ġ�� ī�޶�� ĳ���� ������ �Ÿ��� ���ؼ� ī�޶��� ��ġ�� ����
            Vector3 cameraPosition = target.position + distance;
            // ���ϴ� ȸ�������� ī�޶� ȸ��
            Quaternion cameraRotation = Quaternion.Euler(rotation);

            transform.position = cameraPosition; // ī�޶� ��ġ�� ���� ���� ��ġ�� �̵�
            transform.rotation = cameraRotation; // ī�޶� ������ ���� ������ ȸ��
        }
    }
}
