using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour 
{

    public enum TurretType
    {
        Single = 1,
        Dual = 2,
        Catapult = 3,
        Mortor = 4,
    }

    private GameManager gameM;
    private PlayerController playerC;
    public GameObject currentTarget;
    public Transform turreyHead;

    public float range;
    public float shootCoolDown;
    private float timer;
    public float loockSpeed;
    public LayerMask layerMask;

    public Vector3 randomRot;
    public Animator animator;

    [Header("[Turret Type]")]
    public TurretType turretType = TurretType.Single;

    private List<GameObject> monsters;
    public Transform muzzleMain;
    public Transform muzzleSub;
    public GameObject muzzleEff;
    public GameObject bullet;
    private bool shootLeft = true;

    private Transform lockOnPos;
    //public TurretShoot_Base shotScript;

    void Start () 
    {
        playerC = GameObject.Find("Player").GetComponent<PlayerController>();
        gameM = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (turretType == TurretType.Single)
            range = 5;
        else if (turretType == TurretType.Dual)
            range = 7;
        else if (turretType == TurretType.Catapult)
            range = 15;
        else
            range = 40;

        if (transform.GetChild(0).GetComponent<Animator>())
            animator = transform.GetChild(0).GetComponent<Animator>();

        randomRot = new Vector3(0, Random.Range(0, 359), 0);
    }
	
	void Update () 
    {
        ChackForTarget();

        timer += Time.deltaTime;
	}

    private void ChackForTarget()
    {
        if (!gameM.roundStart || gameM.gameOver)
        {
            IdleRotate();
            return;
        }
        monsters = playerC.monsters;

        float dist = float.MaxValue;
        foreach (GameObject monster in monsters)
        {
            if (!monster)
                continue;

            float targetDist = Vector3.Distance(transform.position, monster.transform.position);

            if (targetDist < dist)
            {
                currentTarget = monster.gameObject;
                dist = targetDist;
            }
        }

        if (currentTarget != null && dist <= range)
            FollowTarget();

        else
            IdleRotate();
    }

    private void FollowTarget()
    {
        if (currentTarget == null)
            return;

        Vector3 targetDir = currentTarget.transform.position - turreyHead.position;
        targetDir.y = 0;

        if (turretType == TurretType.Single || turretType == TurretType.Catapult)
            turreyHead.forward = targetDir;

        else
            turreyHead.transform.rotation = Quaternion.RotateTowards(turreyHead.rotation, Quaternion.LookRotation(targetDir), loockSpeed * Time.deltaTime);

        if (timer >= shootCoolDown)
        {
            timer = 0;

            if (animator != null)
            {
                animator.SetTrigger("Fire");
                ShootTrigger();
            }
            else
                ShootTrigger();
        }
    }

    private void ShootTrigger()
    {
        Shoot(currentTarget);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void IdleRotate()
    {
        bool refreshRandom = false;
        
        if (turreyHead.rotation != Quaternion.Euler(randomRot))
        {
            turreyHead.rotation = Quaternion.RotateTowards(turreyHead.transform.rotation, Quaternion.Euler(randomRot), loockSpeed * Time.deltaTime * 0.2f);
        }
        else
        {
            refreshRandom = true;

            if (refreshRandom)
            {

                int randomAngle = Random.Range(0, 359);
                randomRot = new Vector3(0, randomAngle, 0);
                refreshRandom = false;
            }
        }
    }

    public void Shoot(GameObject go)
    {
        if (currentTarget == null)
            return;

        if (turretType == TurretType.Catapult)
        {
            lockOnPos = go.transform;

            Instantiate(muzzleEff, muzzleMain.transform.position, muzzleMain.rotation);
            GameObject missleGo = Instantiate(bullet, muzzleMain.transform.position, muzzleMain.rotation);
            Projectile projectile = missleGo.GetComponent<Projectile>();
            projectile.target = lockOnPos;
        }

        else if (turretType == TurretType.Mortor)
        {
            lockOnPos = go.transform;

            Instantiate(muzzleEff, muzzleMain.transform.position, muzzleMain.rotation);
            GameObject missleGo = Instantiate(bullet, muzzleMain.transform.position, muzzleMain.rotation);
            Projectile projectile = missleGo.GetComponent<Projectile>();
            projectile.target = lockOnPos;
        }

        else if(turretType == TurretType.Dual)
        {
            if (shootLeft)
            {
                Instantiate(muzzleEff, muzzleMain.transform.position, muzzleMain.rotation);
                GameObject missleGo = Instantiate(bullet, muzzleMain.transform.position, muzzleMain.rotation);
                Projectile projectile = missleGo.GetComponent<Projectile>();
                projectile.target = transform.GetComponent<TurretAI>().currentTarget.transform;
            }
            else
            {
                Instantiate(muzzleEff, muzzleSub.transform.position, muzzleSub.rotation);
                GameObject missleGo = Instantiate(bullet, muzzleSub.transform.position, muzzleSub.rotation);
                Projectile projectile = missleGo.GetComponent<Projectile>();
                projectile.target = transform.GetComponent<TurretAI>().currentTarget.transform;
            }

            shootLeft = !shootLeft;
        }

        else
        {
            Instantiate(muzzleEff, muzzleMain.transform.position, muzzleMain.rotation);
            GameObject missleGo = Instantiate(bullet, muzzleMain.transform.position, muzzleMain.rotation);
            Projectile projectile = missleGo.GetComponent<Projectile>();
            projectile.target = currentTarget.transform;
        }
    }
}
