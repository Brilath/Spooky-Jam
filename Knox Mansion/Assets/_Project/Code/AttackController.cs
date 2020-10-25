using System;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float attackRate;
    [SerializeField] private float nextAttack;

    private void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            if(Time.time >= nextAttack)
            {
                nextAttack = Time.time + attackRate;
                Fire();
            }
        }
    }

    private void Fire()
    {
        Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
    }
}
