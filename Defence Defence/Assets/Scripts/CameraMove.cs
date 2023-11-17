using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target; // 카메라가 따라갈 대상
    public Vector3 distance = new Vector3(0, 15, -4); // 카메라와 케릭터 사이의 거리
    public Vector3 rotation = new Vector3(70, 0, 0); // 카메라의 각도

    private void LateUpdate()
    {
        if(target != null)
        {
            // 캐릭터의 위치에 카메라와 캐릭터 사이의 거리를 더해서 카메라의 위치를 결정
            Vector3 cameraPosition = target.position + distance;
            // 원하는 회전값으로 카메라 회전
            Quaternion cameraRotation = Quaternion.Euler(rotation);

            transform.position = cameraPosition; // 카메라 위치를 위의 변수 위치로 이동
            transform.rotation = cameraRotation; // 카메라 각도를 위의 각도로 회전
        }
    }
}
