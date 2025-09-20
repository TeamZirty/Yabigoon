using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YabigoonController : MonoBehaviour
{
    // 이동 속도, 점프 힘 등 플레이어 스탯
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    // 땅 감지 관련 변수
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    // 대쉬 관련 변수
    public float dashForce = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 3f;

    // 컴포넌트 변수
    private Rigidbody2D rb;
    public Transform gunHolder;
    private Animator animator;
    public DashHUDController dashHUD;

    // 상태 변수
    private bool isGrounded;
    private bool canJump = true;
    private bool canDash = true;
    private bool isDashing = false;
    private float currentDashCooldown = 0f;

    // 대시 방향을 저장할 변수
    private float dashDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (dashHUD != null)
        {
            dashHUD.UpdateCooldown(0f);
        }
    }

    void Update()
    {
        // 마우스 위치를 기반으로 플레이어와 총의 방향 전환
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerToMouse = mousePos - transform.position;
        if (playerToMouse.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            gunHolder.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            gunHolder.localScale = new Vector3(-1, 1, 1);
        }

        // 땅 감지
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && !wasGrounded)
        {
            canJump = true;
        }

        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && canJump && !isDashing)
        {
            Jump();
            canJump = false;
        }

        // 대쉬 쿨타임 처리
        if (!canDash)
        {
            currentDashCooldown -= Time.deltaTime;
            if (currentDashCooldown <= 0)
            {
                canDash = true;
                currentDashCooldown = 0f;
                if (dashHUD != null) dashHUD.UpdateCooldown(0f);
            }
            else
            {
                if (dashHUD != null) dashHUD.UpdateCooldown(currentDashCooldown / dashCooldown);
            }
        }

        // 대쉬
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Horizontal");

        // 대쉬 중이 아닐 때만 일반 이동 적용
        if (!isDashing)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }

        // 애니메이터에 이동 속도 전달
        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        // 키보드 입력이 있을 때만 방향을 갱신 (대시 방향을 위함)
        if (moveInput != 0)
        {
            dashDirection = moveInput;
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        currentDashCooldown = dashCooldown;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        // 키보드 입력이 없다면 플레이어가 바라보는 방향으로 대시
        if (dashDirection == 0)
        {
            dashDirection = transform.localScale.x > 0 ? 1f : -1f;
        }

        // Y축 속도를 유지하면서 대쉬 힘 적용
        rb.velocity = new Vector2(dashDirection * dashForce, rb.velocity.y);

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        rb.gravityScale = originalGravity;
    }

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}