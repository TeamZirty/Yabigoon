using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 좌우 이동 속도
    public float liftSpeed = 5f; // 상하 상승/하강 속도

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() // FixedUpdate에서 물리 연산 처리
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // 키보드 입력을 받아옴
        float moveHorizontal = Input.GetAxis("Horizontal"); // A, D, 왼쪽/오른쪽 화살표
        float moveVertical = Input.GetAxis("Vertical");   // W, S, 위/아래 화살표

        // 풍선이 기본적으로 위로 뜨는 힘 (부력)
        Vector2 buoyantForce = new Vector2(0, 1) * liftSpeed;
        rb.AddForce(buoyantForce);

        // 키 입력에 따른 좌우 이동 힘
        Vector2 horizontalForce = new Vector2(moveHorizontal, 0) * moveSpeed;
        rb.AddForce(horizontalForce);

        // 키 입력에 따른 상하 이동 힘 (누르면 더 빨리 올라가거나 내려감)
        Vector2 verticalForce = new Vector2(0, moveVertical) * liftSpeed;
        rb.AddForce(verticalForce);
    }
}