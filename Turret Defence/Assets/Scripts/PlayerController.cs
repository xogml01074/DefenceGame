using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public enum PlayerState
{
    Idle,
    Move,
    GrassMove,
    WaterMove,
    Attack,
}

public class PlayerController : MonoBehaviour
{
    PlayerState playerState;

    public FixedJoystick joystick;
    public float moveSpeed = 5;
    public float shotRange = 5;
    public float shotDamage = 4.5f;
    public GameObject bullet;
    public Transform firePos;

    public Transform grassCheckTransform;
    public LayerMask grassCheckLayerMask;
    public LayerMask waterCheckLayerMask;

    public Animator animator;
    public GameManager gameM;
    public SoundManager soundM;

    public List<GameObject> monsters; // 몬스터 타겟 리스트
    private Transform targetT;


    private void Update()
    {
        if (gameM.gameOver)
            return;

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
        Vector3 movement = moveDirection.normalized * moveSpeed * Time.deltaTime;

        if (moveDirection != Vector3.zero)
        {
            animator.SetBool("playerMove", true);
            transform.rotation = Quaternion.LookRotation(moveDirection);
            transform.position += movement;

            UpdateCollider();
        }
        else
        {
            playerState = PlayerState.Idle;
            animator.SetBool("playerMove", false);
        }
    }

    private void UpdateCollider()
    {
        // 플레이어 밑에 가상의 박스와 부딪히는 Ground 레이어 체크후 부딪히는 레이어의 개 수 반환
        Collider[] grassCol =
            Physics.OverlapBox(grassCheckTransform.position, new Vector3(0.2f, 0.2f, 0.2f), Quaternion.identity, grassCheckLayerMask);

        Collider[] waterCol =
            Physics.OverlapBox(grassCheckTransform.position , new Vector3(0.2f, 0.3f, 0.2f), Quaternion.identity, waterCheckLayerMask);

        if (waterCol.Length > 0)
        {
            playerState = PlayerState.WaterMove;
            moveSpeed = 1.5f;
            animator.SetBool("playerOnWater", true);
            animator.SetBool("playerOnGround", false);
        }
        else if (grassCol.Length > 0)
        {
            playerState = PlayerState.GrassMove;
            moveSpeed = 2.5f;
            animator.SetBool("playerOnGround", true);
            animator.SetBool("playerOnWater", false);
        }
        else
        {
            playerState = PlayerState.Move;
            moveSpeed = 5;
            animator.SetBool("playerOnGround", false);
            animator.SetBool("playerOnWater", false);
        }

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
            animator.ResetTrigger("gunFire");
            return;
        }

        if (playerState == PlayerState.Attack)
            animator.SetTrigger("gunFire");
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BossMonster"))
            gameM.playerLife -= 2;
    }
}
