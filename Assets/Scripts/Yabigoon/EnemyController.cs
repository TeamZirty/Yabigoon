using UnityEngine;
using System.Collections;
public class EnemyController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator; // Animator 컴포넌트 변수 추가

    public EnemyHealthBar healthBar;

    // 적의 기본 스탯
    public float moveSpeed = 3f;
    public int maxHealth = 100;
    public int currentHealth;
    public int goldOnDeath = 10; // 죽었을 때 드롭할 골드 양

    private Transform player; // 플레이어 위치를 추적할 변수

    [Header("애니메이션")]
    public float flashDuration = 0.1f; // 번쩍이는 시간





    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetHealth(1f);
        }
        // 씬에서 플레이어를 찾아 참조
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // Animator 컴포넌트 가져오기
    }

    void Update()
    {
        // 플레이어가 존재하는지 확인
        if (player != null)
        {
            // 플레이어 방향으로 이동
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    // 외부(총알)에서 호출될 데미지 함수
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("적이 데미지를 입었습니다! 남은 체력: " + currentHealth);

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

    // 적이 죽었을 때 실행되는 함수
    void Die()
    {
        // 골드 획득
        if (PlayerStatus.Instance != null)
        {
            PlayerStatus.Instance.AddGold(goldOnDeath);
        }

        // 적 오브젝트 파괴
        Destroy(gameObject);
    }

    
}