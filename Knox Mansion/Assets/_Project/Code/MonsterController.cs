using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float nextAttack;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private int attackAmount;
    [SerializeField] private float attackTime; 
    [SerializeField] private float maxSpeed;
    [SerializeField] private bool hasTarget;
    [SerializeField] private LayerMask playerMask;

    private void Update()
    {
        if(hasTarget)
        {
            HandleMovement();
        }
    }


    private void HandleMovement()
    {
        if (Vector3.Distance(transform.position, target.position) > attackRange)
        {

            Vector3 moveDir = (target.position - transform.position).normalized;
            transform.position = transform.position + moveDir * maxSpeed * Time.deltaTime;

            var dir = target.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            Attack();
        }
    }

    private void Attack()
    {
        Debug.Log($"Attacking Player");
        Collider2D player = Physics2D.OverlapCircle(transform.localPosition, attackRange, playerMask);
        if (player == null) return;

        Debug.Log($"actAttacking Player");

        if (CanAttack())
        {
            player.GetComponent<Health>().TakeDamage(attackAmount);
            nextAttack = Time.time + attackSpeed;
            StartCoroutine(ChillOut());
        }
    }

    private bool CanAttack()
    {
        return Time.time >= nextAttack;
    }

    private IEnumerator ChillOut()
    {
        yield return new WaitForSeconds(attackTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            target = collision.gameObject.transform;
            hasTarget = true;
        }
    }
}
