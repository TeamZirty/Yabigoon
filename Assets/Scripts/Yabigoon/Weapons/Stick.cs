using UnityEngine;

public class Stick : WeaponBase
{
    public Transform attackPoint; // A point in front of the player where the swing happens
    public float attackRange = 0.8f;
    public LayerMask enemyLayers; // Set this in the Inspector to define what is an enemy
    public AudioClip swingSound;

    public override void Attack()
    {
        // Note: A full implementation would also trigger a swing animation.

        // Detect all colliders within a circle at the attackPoint that are on the enemyLayers
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Loop through all enemies that were hit
        foreach(Collider2D enemyCollider in hitEnemies)
        {
            EnemyController enemy = enemyCollider.GetComponent<EnemyController>();
            if (enemy != null)
            {
                // Use the damage value from the linked WeaponItem ScriptableObject
                enemy.TakeDamage(weaponData.damage);
            }
        }

        // Play a swing sound
        if (SoundManager.Instance != null && swingSound != null)
        {
            SoundManager.Instance.PlaySFX(swingSound);
        }
    }

    // This is a helper that draws a red circle in the editor so you can see the attack range.
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
