using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private float forceAmount;
    [SerializeField] private float lifeSpan;
    [SerializeField] private int damage;
    [SerializeField] private bool playerBullet;

    private void Start()
    {
        body.AddForce(transform.up * forceAmount, ForceMode2D.Impulse);
        Destroy(gameObject, lifeSpan);     
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerBullet)
        {
            if (string.Compare(collision.gameObject.tag, "Monster") == 0)
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
                collision.gameObject.GetComponent<MonsterController>().Agro();
            }
            else if(string.Compare(collision.gameObject.tag, "Boss") == 0)
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
        }
        else
        {
            if (string.Compare(collision.gameObject.tag, "Player") == 0 || string.Compare(collision.gameObject.tag, "Monster") == 0)
            {
                Debug.Log($"Hurting {gameObject.tag}");
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
