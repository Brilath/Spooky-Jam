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

    private void Awake()
    {
        Health.OnDamageTaken += HandleDamageTaken;
    }

    private void OnDestroy()
    {
        Health.OnDamageTaken -= HandleDamageTaken;
    }

    private void Update()
    {
        if(hasTarget)
        {
            HandleMovement();
        }
    }

    private void HandleDamageTaken()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        hasTarget = true;
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
        else if(CanAttack())
        {
            Attack();
        }
    }

    private void Attack()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.localPosition, attackRange, playerMask);
        if (player == null) return;        

        player.GetComponent<Health>().TakeDamage(attackAmount);
        nextAttack = Time.time + attackSpeed;
    }

    private bool CanAttack()
    {
        return Time.time >= nextAttack;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            target = collision.gameObject.transform;
            hasTarget = true;
        }
    }

    public void Agro()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
        if(target != null)
            hasTarget = true;
    }
}
