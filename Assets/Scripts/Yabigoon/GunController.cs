using UnityEngine;

public class GunController : MonoBehaviour
{
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerToMouse = mousePos - transform.parent.position;

        float angle = Mathf.Atan2(playerToMouse.y, playerToMouse.x) * Mathf.Rad2Deg;

        // 플레이어의 좌우 방향을 확인
        bool isFacingRight = (transform.parent.localScale.x > 0);

        if (isFacingRight)
        {
            // 플레이어가 오른쪽을 볼 때: 마우스 방향 그대로 회전
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            // 플레이어가 왼쪽을 볼 때: 총의 Y축을 기준으로 뒤집고 각도를 보정
            transform.rotation = Quaternion.Euler(180, 0, -angle);
        }
    }
}