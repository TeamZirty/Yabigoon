// Bullet.cs 스크립트
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10; // 총알 데미지

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트가 적인지 확인
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage); // 적에게 데미지 적용
        }

        // 총알 오브젝트 풀로 반환
        // Destroy(gameObject); // (테스트용)
        ObjectPoolManager.Instance.ReturnBullet(gameObject);
    }
}