using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTurretRotate : MonoBehaviour
{
    public Vector3 randomRot;
    public Transform turreyHead;
    public float loockSpeed;

    private void Start()
    {
        randomRot = new Vector3(0, Random.Range(0, 359), 0);
    }

    private void Update()
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
}
