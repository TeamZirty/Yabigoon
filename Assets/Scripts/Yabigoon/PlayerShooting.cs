using UnityEngine;
using System.Collections;
public class PlayerShooting : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public Transform firePoint;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 오브젝트 풀에서 총알 가져오기
        GameObject bullet = ObjectPoolManager.Instance.GetBullet();
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;

        // 총알 이동
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed;

        // 10초 후 총알 비활성화 및 풀로 반환
        StartCoroutine(DeactivateBulletAfterTime(bullet, 10f));
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