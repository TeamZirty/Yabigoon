using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public EnemyHealthBar healthBar;

    public float moveSpeed = 3f;
    public int maxHealth = 100;
    public int currentHealth;
    public int goldOnDeath = 10;

    private Transform player;

    [Header("애니메이션")]
    public float flashDuration = 0.1f;

    private bool isFloating = false;
    private Rigidbody2D rb;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetHealth(1f);
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isFloating) return;

        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    public void FloatAway()
    {
        if (!isFloating)
        {
            StartCoroutine(FloatAndFall());
        }
    }

    IEnumerator FloatAndFall()
    {
        isFloating = true;
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        rb.velocity = Vector2.zero; // Reset velocity

        int originalLayer = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("FloatingEnemy");

        float originalMoveSpeed = moveSpeed;
        float originalGravity = rb.gravityScale;
        moveSpeed = 0;
        rb.gravityScale = -0.2f; // Float up

        float floatStartTime = Time.time;
        float startY = transform.position.y;

        while (Time.time < floatStartTime + 5f && transform.position.y < startY + 6f)
        {
            yield return null;
        }

        Bullet balloon = GetComponentInChildren<Bullet>();
        if (balloon != null && balloon.isBalloon)
        {
            balloon.transform.SetParent(null);
            ObjectPoolManager.Instance.ReturnBullet(balloon.gameObject);
        }

        // Restore state
        gameObject.layer = originalLayer;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.gravityScale = originalGravity;
        moveSpeed = originalMoveSpeed;
        isFloating = false;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("적이 데미지를 입었습니다! 현재 체력: " + currentHealth);

        if (animator != null)
        {
            animator.SetTrigger("Damaged");
        }

        if (healthBar != null)
        {
            float healthFraction = (float)currentHealth / maxHealth;
            healthBar.SetHealth(healthFraction);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (PlayerStatus.Instance != null)
        {
            PlayerStatus.Instance.AddGold(goldOnDeath);
        }
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.enemyDeathSFX);
        }
        Destroy(gameObject);
    }
}