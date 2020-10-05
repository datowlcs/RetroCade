using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    BulletPool bPool;
    public Transform fpCamera;
    public Transform firePoint;

    public float firePower = 10;

    public bool isShooting;
    public float fireSpeed;
    public float fireTimer;


    // Start is called before the first frame update
    void Start()
    {
        bPool = BulletPool.main;
    }
    public void Shoot()
    {
        Vector3 bulletVelocity = fpCamera.forward * firePower;
        bPool.PickFromPool(firePoint.position, bulletVelocity);
    }

    public void PullTrigger()
    {
        if (fireSpeed > 0) isShooting = true;

        else Shoot();

    }

    public void ReleaseTrigger()
    {
        isShooting = false;

        fireTimer = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if(isShooting)
        {
            if (fireTimer > 0) fireTimer -= Time.deltaTime;

            else
            {
                fireTimer = fireSpeed;
                Shoot();
            }
        }
    }
}
