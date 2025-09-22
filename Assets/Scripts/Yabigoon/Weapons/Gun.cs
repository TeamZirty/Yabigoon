using UnityEngine;
using System.Collections;

public class Gun : WeaponBase
{
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public AudioClip shootSound;

    private bool isBalloonMode = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isBalloonMode = !isBalloonMode;
            Debug.Log("Balloon Mode: " + isBalloonMode);
        }
    }

    public override void Attack()
    {
        if (weaponData == null)
        {
            // This check is kept just in case, to prevent future errors.
            Debug.LogError("GUN ERROR: The 'Weapon Data' asset (e.g., GunData) is not assigned in the Inspector on the Gun prefab!");
            return;
        }

        GameObject bulletObject = ObjectPoolManager.Instance.GetBullet();
        bulletObject.transform.position = firePoint.position;
        bulletObject.transform.rotation = firePoint.rotation;

        Bullet bullet = bulletObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.isBalloon = isBalloonMode;
            bullet.SetDamage(weaponData.damage);
        }

        Rigidbody2D rb = bulletObject.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed;

        if (!isBalloonMode)
        {
            StartCoroutine(DeactivateBulletAfterTime(bulletObject, 6f));
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
