using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public enum PlayerState
{
    Idle,
    Move,
    SlowMove,
    Attack,
}

public class PlayerController : MonoBehaviour
{
    PlayerState playerState;

    public FixedJoystick joystick;
    public float speed = 6;
    public float shotRange = 2;
    public float shotDamage = 3;
    public GameObject bullet;
    public Transform firePos;

    public Transform grassCheckTransform;
    public LayerMask grassCheckLayerMask;

    public Animator animator;
    public GameManager gameM;

    public List<GameObject> monsters; // 몬스터 타겟 리스트
    private Transform targetT;


    private void Update()
    {
        Attack();
        PlayerMove();
        MonsterTarget();
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
        Vector3 movement = moveDirection.normalized * speed * Time.deltaTime;

        if (moveDirection != Vector3.zero)
        {
            animator.SetBool("playerMove", true);
            transform.rotation = Quaternion.LookRotation(moveDirection);
            transform.position += movement;

            if (UpdateColliderGrassStatus() > 0)
            {
                playerState = PlayerState.SlowMove;
                speed = 3;
                animator.SetBool("playerOnGround", true);
            }
            else
            {
                playerState = PlayerState.Move;
                speed = 6;
                animator.SetBool("playerOnGround", false);
            }
        }
        else
        {
            playerState = PlayerState.Idle;
            animator.SetBool("playerMove", false);
        }
    }

    private int UpdateColliderGrassStatus()
    {
        // 플레이어 밑에 가상의 박스와 부딪히는 Ground 레이어 체크후 부딪히는 레이어의 개 수 반환
        Collider[] hitColliders =
            Physics.OverlapBox(grassCheckTransform.position, new Vector3(0.2f, 0.1f, 0.2f), Quaternion.identity, grassCheckLayerMask);

        return hitColliders.Length;
    }

    // 가장 가까운 적 타게팅 
    private void MonsterTarget()
    {
        if (!gameM.roundStart)
            return;

        if (monsters.Count > 0)
        {
            Transform target = null;
            float maxDis = float.MaxValue;
            foreach (var monster in monsters)
            {
                if (!monster)
                    continue;

                float targetdistance = Vector3.Distance(transform.position, monster.transform.position);

                if (targetdistance < maxDis)
                {
                    maxDis = targetdistance;
                    target = monster.transform;
                }
            }

            if (target != null && maxDis <= shotRange)
            {
                targetT = target;
                GunAim();
            }
            else
                targetT = null;
        }
    }

    // 적 조준
    private void GunAim()
    {
        if (!gameM.roundStart || targetT == null)
            return;

        if (playerState == PlayerState.Idle)
        {
            playerState = PlayerState.Attack;

            Vector3 target = new Vector3(targetT.position.x, 2f, targetT.position.z);
            transform.LookAt(target);
        }
    }

    private void Attack()
    {
        if (targetT == null)
        {
            speed = 6;
            animator.ResetTrigger("gunFire");
            return;
        }
        if (playerState == PlayerState.Attack)
        {
            speed = 0;
            animator.SetTrigger("gunFire"); // 총쏘다 움직이게 되면 사격 애니매이션인 채로 움직임
        }
    }

    // 애니메이션 이벤트에 참조연결 
    private void Fire()
    {
        if (targetT == null)
            return;

        Vector3 dir = targetT.position - transform.position; dir.y = 0f;

        Instantiate(bullet, firePos.position, Quaternion.LookRotation(dir));
        playerState = PlayerState.Idle;
    }
}
