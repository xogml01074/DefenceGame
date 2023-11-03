using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    public Transform grassCheckTransform;
    public LayerMask grassCheckLayerMask;

    public Animator animator;
    public GameManager gameM;

    public List<GameObject> monsters; // 몬스터 타겟 리스트
    private Transform targetT;
    private GameObject targets;


    private void Update()
    {
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
        Vector3 movement = moveDirection * speed * Time.deltaTime;

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

    // 가장 가까운 적 타게팅 하는거 손봐야함
    private void MonsterTarget()
    {
        if (!gameM.roundStart)
            return;

        if (monsters.Count > 0)
        {
            GameObject _target = null;
            Transform target = null;
            float maxDis = float.MaxValue;
            foreach (var monster in monsters)
            {
                float targetdistance = Vector3.Distance(transform.position, monster.transform.position);

                if (targetdistance < maxDis)
                {
                    _target = monster;
                    target = monster.transform;
                    maxDis = targetdistance;
                }
            }

            targets = _target;
            targetT = target.transform;
            GunAim();
        }
    }

    private void GunAim()
    {
        float targetDistance = Vector3.Distance(transform.position, targetT.position);
        if (targetDistance < shotRange && playerState == PlayerState.Idle)
        {
            playerState = PlayerState.Attack;
            transform.LookAt(targetT);
            speed = 0;
            Attack();
        }
        else
            speed = 6;
    }

    private void Attack()
    {
        if (monsters.Count <= 0)
            return;

        Monsters monster = targets.GetComponent<Monsters>();

        animator.SetTrigger("gunFire");
        monster.Hurt(shotDamage);

        playerState = PlayerState.Idle;
    }
}
