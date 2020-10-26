using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float nextAttack;
    [SerializeField] private float attackSpeed;
    [SerializeField] private bool hasTarget;
    [SerializeField] private Transform firePoints;
    [SerializeField] private GameObject bulletPrefab;

    private void Update()
    {
        if (hasTarget)
        {
            if (CanAttack())
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        foreach(Transform firePoint in firePoints)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
        nextAttack = Time.time + attackSpeed;
    }

    private bool CanAttack()
    {
        return Time.time >= nextAttack;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = collision.gameObject.transform;
            hasTarget = true;
        }
    }
}
