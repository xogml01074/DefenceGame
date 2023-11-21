using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public TurretAI.TurretType type;
    public Transform target;
    public bool cLockOn;
    public bool mLockOn;

    public float speed = 1;
    public float turnSpeed = 1;
    public bool catapult;
    public bool mortor;
    public LayerMask monster;

    public float knockBack = 0.2f;

    public ParticleSystem explosion;

    private void Start()
    {
        if (catapult)
            cLockOn = true;
        if (mortor)
            mLockOn = true;

        if (type == TurretAI.TurretType.Single)
        {
            if (!target)
                return;

            Vector3 dir = target.position - transform.position;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    private void Update()
    {
        if (transform.position.y <= 0f)
            Explosion();

        if (type == TurretAI.TurretType.Catapult)
        {
            if (cLockOn)
            {
                if (!target)
                    return;

                Vector3 Vo = CalculateCatapultAndMortor(target.transform.position, transform.position, 1);

                transform.GetComponent<Rigidbody>().velocity = Vo;
                cLockOn = false;
            }
        }
        else if(type == TurretAI.TurretType.Mortor)
        {
            if (mLockOn)
            {
                if (!target)
                    return;

                Vector3 Vo = CalculateCatapultAndMortor(target.transform.position, transform.position, 1.3f);

                transform.GetComponent<Rigidbody>().velocity = Vo;
                mLockOn = false;
            }
        }
        else if(type == TurretAI.TurretType.Dual)
        {
            if (!target)
            {
                Explosion();
                return;
            }

            Vector3 dir = target.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, dir, Time.deltaTime * turnSpeed, 0.0f);
            Debug.DrawRay(transform.position, newDirection, Color.red);

            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.LookRotation(newDirection);

        }
        else if (type == TurretAI.TurretType.Single)
        {
            if (!target)
            {
                Explosion();
                return;
            }

            float singleSpeed = speed * Time.deltaTime;
            transform.Translate(transform.forward * singleSpeed * 2, Space.World);
        }
    }

    Vector3 CalculateCatapultAndMortor(Vector3 target, Vector3 origen, float time)
    {
        Vector3 distance = target - origen;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

    private void OnTriggerEnter(Collider other)
    {
        Explosion();
    }

    public void Explosion()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        if (type == TurretAI.TurretType.Mortor)
        {
            Collider[] colls2 = Physics.OverlapSphere(gameObject.transform.position, 4f);
            foreach (Collider coll in colls2)
            {
                if (coll.gameObject.tag == "Monster")
                    coll.gameObject.GetComponent<Monsters>().MortorHurt();

                else if (coll.gameObject.tag == "BossMonster")
                    coll.gameObject.GetComponent<BossMonsterAI>().MortorHurt();
            }
        }

        else if (type == TurretAI.TurretType.Catapult)
        {
            Collider[] colls1 = Physics.OverlapSphere(gameObject.transform.position, 2f);
            foreach (Collider coll in colls1)
            {
                if (coll.gameObject.tag == "Monster")
                    coll.gameObject.GetComponent<Monsters>().CatapultHurt();

                else if (coll.gameObject.tag == "BossMonster")
                    coll.gameObject.GetComponent<BossMonsterAI>().CatapultHurt();
            }
        }
        Destroy(gameObject);
    }
}
