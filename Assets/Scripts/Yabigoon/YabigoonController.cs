using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YabigoonController : MonoBehaviour
{

    // 이동 속도를 조절할 변수
    public float moveSpeed = 5f;

    // Rigidbody 2D 컴포넌트 변수
    private Rigidbody2D rb;
    public Transform gunHolder;
    // Start() 함수에서 Rigidbody 2D 컴포넌트 가져오기
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerToMouse = mousePos - transform.position;

        // 플레이어 방향 전환 (오른쪽: 1, 왼쪽: -1)
        if (playerToMouse.x > 0)
        {
            // 플레이어가 오른쪽을 볼 때
            transform.localScale = new Vector3(1, 1, 1);
            // 총도 오른쪽을 보도록 로컬 스케일 조정
            gunHolder.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            // 플레이어가 왼쪽을 볼 때
            transform.localScale = new Vector3(-1, 1, 1);
            // 총을 Y축으로 뒤집어서 왼쪽을 보게 함
            gunHolder.localScale = new Vector3(-1, 1, 1);
        }
    }

    void FixedUpdate()
    {
        // 키보드 입력 받기 (Horizontal: A, D 또는 ←, →)
        float moveInput = Input.GetAxis("Horizontal");

        // Rigidbody 2D의 속도(velocity)를 직접 조절하여 이동
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

}
