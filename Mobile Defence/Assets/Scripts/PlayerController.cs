using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public FixedJoystick joystick;
    public float speed = 6;
    public float shotRange = 10;
    private float shortDis = float.MaxValue;
    private Transform monsterT;

    public Transform grassCheckTransform;
    public LayerMask grassCheckLayerMask;

    public Animator animator;
    public GameManager gameM;

    public List<Transform> monsters; // 몬스터 타겟 리스트


    private void Update()
    {
        MonsterTargeting();
        PlayerMove();
    }
    private void PlayerMove()
    {
#if UNITY_ANDROID || UNITY_IOS
        float h = joystick.Horizontal; // 조이스틱의 수평 입력 값
        float v = joystick.Vertical; // 조이스틱의 수직 입력 값
#else
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
#endif
        Vector3 moveDirection = new Vector3(h, 0, v);
        Vector3 movement = moveDirection * speed * Time.deltaTime;

        if (moveDirection != Vector3.zero)
        {
            animator.SetBool("playerMove", true);
            transform.rotation = Quaternion.LookRotation(moveDirection);
            transform.position += movement;

            if (UpdateColliderGrassStatus() > 0)
            {
                speed = 3;
                animator.SetBool("playerOnGround", true);
            }
            else
            {
                speed = 6;
                animator.SetBool("playerOnGround", false);
            }
        }
        else
            animator.SetBool("playerMove", false);
    }

    private int UpdateColliderGrassStatus()
    {
        // 플레이어 밑에 가상의 박스와 부딪히는 Ground 레이어 체크후 부딪히는 레이어의 개수 반환
        Collider[] hitColliders =
            Physics.OverlapBox(grassCheckTransform.position, new Vector3(0.2f, 0.1f, 0.2f), Quaternion.identity, grassCheckLayerMask);

        return hitColliders.Length;
    }

    // 가장 가까운 적 타게팅 하는거 손봐야함
    private void MonsterTargeting()
    {
        if (gameM.roundStart && monsters.Count > 0)
        {
            foreach (Transform monster in monsters)
            {
                float distance = Vector3.Distance(transform.position, monster.position);

                if (distance < shortDis)
                {
                    shortDis = distance;
                    monsterT = monster;
                }
            }
            if (shortDis <= shotRange)
                GunFire(true);
            else
                GunFire(false);
        }

    }

    private void GunFire(bool targeting)
    {
        if (targeting)
        {
            transform.LookAt(monsterT.position);
            animator.SetBool("gunFire", true);
        }
        else
        {
            animator.SetBool("gunFire", false);
        }
    }
}
