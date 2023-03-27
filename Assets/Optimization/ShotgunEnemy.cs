using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunEnemy : MonoBehaviour
{
    [SerializeField]
    private int bullets = 5;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    GameObject bulletParticle;

    private void Start()
    {
        InvokeRepeating("Shoot", 1, 1);
    }

    [ContextMenu("Shoot")]
    void Shoot()
    {
        for (int i = 0; i < bullets; i++)
        {
            Quaternion aimRotation = Quaternion.Euler(transform.right) * Quaternion.Euler(0,0,Random.Range(-30,30));
            Instantiate(bulletPrefab, transform.position, aimRotation);
            Instantiate(bulletParticle, transform.position, aimRotation);
        }
    }
}
