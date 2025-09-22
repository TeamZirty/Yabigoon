// Bullet.cs 
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage = 10; // Default damage

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
    public bool isBalloon = false; //  

    private Rigidbody2D rb;
    private Collider2D col;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void OnEnable()
    {
        //      
        if (rb != null) rb.isKinematic = false;
        if (col != null) col.enabled = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        if (enemy != null)
        { 
            if (isBalloon)
            {
                //  
                enemy.FloatAway();
                transform.SetParent(collision.transform); //   
                rb.isKinematic = true; //      
                col.enabled = false; //   
            }
            else
            {
                //  
                enemy.TakeDamage(damage);
                ObjectPoolManager.Instance.ReturnBullet(gameObject);
            }
        }
        else
        {
            //      
            ObjectPoolManager.Instance.ReturnBullet(gameObject);
        }
    }
}