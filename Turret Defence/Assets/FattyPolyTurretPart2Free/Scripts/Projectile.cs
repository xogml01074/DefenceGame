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
            Vector3 dir = target.position - transform.position;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    private void Update()
    {
        if (type != TurretAI.TurretType.Mortor && type != TurretAI.TurretType.Catapult)
        {
            if (target == null)
            {
                Explosion();
                return;
            }
        }
        if (transform.position.y <= 2f)
            Explosion();

        if (type == TurretAI.TurretType.Catapult)
        {
            if (cLockOn)
            {
                Vector3 Vo = CalculateCatapultAndMortor(target.transform.position, transform.position, 1);

                transform.GetComponent<Rigidbody>().velocity = Vo;
                cLockOn = false;
            }
        }
        else if(type == TurretAI.TurretType.Mortor)
        {
            if (mLockOn)
            {
                Vector3 Vo = CalculateCatapultAndMortor(target.transform.position, transform.position, 1.2f);

                transform.GetComponent<Rigidbody>().velocity = Vo;
                mLockOn = false;
            }
        }
        else if(type == TurretAI.TurretType.Dual)
        {
            Vector3 dir = target.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, dir, Time.deltaTime * turnSpeed, 0.0f);
            Debug.DrawRay(transform.position, newDirection, Color.red);

            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.LookRotation(newDirection);

        }
        else if (type == TurretAI.TurretType.Single)
        {
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
        if (type != TurretAI.TurretType.Mortor && type != TurretAI.TurretType.Catapult)
            Explosion();
        else if (other.gameObject == CompareTag("Map"))
            Explosion();
    }

    public void Explosion()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
