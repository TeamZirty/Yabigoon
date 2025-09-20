using UnityEngine;

// EnemyHealth.cs
public class EnemyHealth : MonoBehaviour
{
    public int goldOnDeath = 10;
    public int maxHealth = 100; // 최대 체력
    private int currentHealth; // 현재 체력

    void Start()
    {
        currentHealth = maxHealth;
    }

    // 외부에서 데미지를 받는 함수
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 적이 죽을 때 실행되는 함수
    void Die()
    {
        PlayerStatus.Instance.AddGold(goldOnDeath);
        Debug.Log("적이 처치되어 골드를 획득했습니다.");
        // 파괴 대신 오브젝트 풀링으로 비활성화하거나, 애니메이션 재생 등 추가 로직을 넣을 수 있습니다.
        Destroy(gameObject);
    }
}