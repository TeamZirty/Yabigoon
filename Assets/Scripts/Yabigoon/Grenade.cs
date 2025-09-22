using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour
{
    [Header("Grenade Properties")]
    public float explosionRadius = 3f;
    public int explosionDamage = 50;
    public float explosionDelay = 3f; // Time before explosion, 0 for impact explosion
    public LayerMask enemyLayers;

    [Header("Effects")]
    public GameObject explosionEffectPrefab;
    public AudioClip explosionSound;

    private bool hasExploded = false;

    void Start()
    {
        if (explosionDelay > 0)
        {
            StartCoroutine(ExplosionTimer());
        }
    }

    IEnumerator ExplosionTimer()
{
        yield return new WaitForSeconds(explosionDelay);
        Explode();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Explode on impact if delay is 0 and it hasn't exploded yet
        if (explosionDelay == 0 && !hasExploded)
        {
            Explode();
        }
    }

    void Explode()
    {
        if (hasExploded) return; // Prevent multiple explosions
        hasExploded = true;

        // Instantiate visual and audio effects
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }
        if (explosionSound != null && SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySFX(explosionSound);
        }

        // Find enemies within the explosion radius
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayers);

        // Deal damage to hit enemies
        foreach (Collider2D enemyCollider in hitEnemies)
        {
            EnemyController enemy = enemyCollider.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(explosionDamage);
            }
        }

        // Destroy the grenade object after explosion
        Destroy(gameObject);
    }

    // Optional: Draw a gizmo in the editor to visualize the explosion radius
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}