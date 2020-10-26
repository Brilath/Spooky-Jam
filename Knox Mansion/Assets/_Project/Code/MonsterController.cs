using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource attackSource;
    [SerializeField] private AudioClip agroSound;
    [SerializeField] private AudioClip[] hurtSounds;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip chompSound;
    [SerializeField] private CircleCollider2D bodyCollider2D;
    [SerializeField] private BoxCollider2D targetCollider2D;
    [SerializeField] private SpriteRenderer bodyRenderer;
    [SerializeField] private SpriteRenderer headRenderer;
    [SerializeField] private SpriteRenderer tailRenderer;
    [SerializeField] private Color deathColor;
    [SerializeField] private Animator anim;
    

    private void Awake()
    {
        Health.OnDeath += HandleDeath;
        Health.OnDamageTaken += HandleDamageTaken;
    }

    private void OnDestroy()
    {
        Health.OnDeath -= HandleDeath;
        Health.OnDamageTaken -= HandleDamageTaken;
    }

    private void Update()
    {
        if(hasTarget)
        {
            HandleMovement();
        }
        else
        {
            anim.SetFloat("speed", 0);
        }
    }

    private void HandleDeath(GameObject deadObject)
    {
        if (gameObject != deadObject) return;
        hasTarget = false;
        bodyRenderer.color = deathColor;
        headRenderer.color = deathColor;
        tailRenderer.color = deathColor;
        bodyCollider2D.enabled = false;
        targetCollider2D.enabled = false;
        audioSource.PlayOneShot(deathSound);
        anim.SetFloat("speed", 0);
        this.enabled = false;
    }

    private void HandleDamageTaken()
    {
        Agro();
    }

    private void HandleMovement()
    {
        if (Vector3.Distance(transform.position, target.position) > attackRange)
        {

            Vector3 moveDir = (target.position - transform.position).normalized;
            transform.position = transform.position + moveDir * maxSpeed * Time.deltaTime;

            anim.SetFloat("speed", maxSpeed);

            var dir = target.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else if(CanAttack())
        {
            anim.SetTrigger("attack");
            attackSource.PlayOneShot(chompSound);
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
        if (!hasTarget)
            audioSource.PlayOneShot(agroSound);
        else
        {
            int rand = Random.Range(0, hurtSounds.Length - 1);
            audioSource.PlayOneShot(hurtSounds[rand]);
        }

        if (target != null)
            hasTarget = true;
    }
}
