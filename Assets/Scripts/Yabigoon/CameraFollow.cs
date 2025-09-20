using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // 플레이어 오브젝트를 연결할 변수
    public Transform player;

    // 카메라의 Z축 위치를 고정할 변수
    public float zOffset = -10f;

    // 부드러운 움직임을 위한 변수
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        // 플레이어의 위치를 따라갈 목표 위치 설정
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, zOffset);

        // 현재 카메라 위치에서 목표 위치로 부드럽게 이동
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
