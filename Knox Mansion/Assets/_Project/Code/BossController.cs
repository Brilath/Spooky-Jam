using System;
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
    [SerializeField] private bool hasCalledOut;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip callSound;
    [SerializeField] private AudioClip hurtSound;


    private void Awake()
    {
        Health.OnDamageTaken += HandleDamageTaken;
        Health.OnDeath += HandleDeath;
    }

    private void OnDestroy()
    {
        Health.OnDamageTaken -= HandleDamageTaken;
        Health.OnDeath -= HandleDeath;
    }

    private void HandleDamageTaken()
    {
        if(!hasCalledOut)
            audioSource.PlayOneShot(callSound);
        else
            audioSource.PlayOneShot(hurtSound);
    }

    private void HandleDeath(GameObject deadObject)
    {
        if (gameObject != deadObject) return;
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        audioSource.PlayOneShot(hurtSound);
        yield return new  WaitForSeconds(1);
        Destroy(gameObject);
    }

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
