using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private float forceAmount;
    [SerializeField] private float lifeSpan;
    [SerializeField] private int damage;

    private void Start()
    {
        body.AddForce(transform.up * forceAmount, ForceMode2D.Impulse);
        Destroy(gameObject, lifeSpan);     
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(string.Compare(collision.gameObject.tag, "Monster") == 0)
    //    {
    //        collision.GetComponent<Health>().TakeDamage(damage);
    //    }

    //    Destroy(gameObject);
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Bullet hit {collision.gameObject.tag}");
        if (string.Compare(collision.gameObject.tag, "Monster") == 0)
        {
            Debug.Log("Hurt monster");
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
