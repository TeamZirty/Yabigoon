using UnityEngine;
using System.Collections;

public class Shotgun : WeaponBase
{
    [Header("Shotgun Specifics")]
    public int pelletCount = 5;
    public float spreadAngle = 20f;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public AudioClip shootSound;

    public override void Attack()
    {
        if (weaponData == null)
        {
            Debug.LogError("SHOTGUN ERROR: The 'Weapon Data' asset is not assigned in the Inspector on the Shotgun prefab!");
            return;
        }

        for (int i = 0; i < pelletCount; i++)
        {
            GameObject bulletObject = ObjectPoolManager.Instance.GetBullet();
            bulletObject.transform.position = firePoint.position;

            // Start with the firePoint's direction (which is rotated by GunController)
            Vector2 direction = firePoint.right;

            // Calculate a random angle for spread
            float randomAngle = Random.Range(-spreadAngle / 2, spreadAngle / 2);
            
            // Rotate the direction vector by the random angle
            Quaternion spreadRotation = Quaternion.AngleAxis(randomAngle, Vector3.forward);
            Vector2 spreadDirection = spreadRotation * direction;

            // Set the bullet's rotation to look in the new direction
            bulletObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, spreadDirection);

            Bullet bullet = bulletObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.isBalloon = false; // Shotguns don't shoot balloons
                bullet.SetDamage(weaponData.damage);
            }

            Rigidbody2D rb = bulletObject.GetComponent<Rigidbody2D>();
            rb.velocity = spreadDirection * bulletSpeed;

            StartCoroutine(DeactivateBulletAfterTime(bulletObject, 3f));
        }

        if (SoundManager.Instance != null && shootSound != null)
        {
            SoundManager.Instance.PlaySFX(shootSound);
        }
    }

    IEnumerator DeactivateBulletAfterTime(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        if (bullet.activeSelf)
        {
            ObjectPoolManager.Instance.ReturnBullet(bullet);
        }
    }
}