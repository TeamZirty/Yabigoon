using UnityEditor.Tilemaps;
using UnityEngine;

public class GravityFlip : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Rigidbody2D 컴포넌트 가져오기
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Q 키를 눌렀을 때
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // gravityScale의 부호를 반전시켜 중력 방향을 바꿉니다.
            rb.gravityScale *= -1;

            // 플레이어의 Y축 회전을 180도 반전시켜 캐릭터를 뒤집습니다.
            // (선택 사항: 시각적인 효과를 위해)
            spriteRenderer.flipY = !spriteRenderer.flipY;
            Debug.Log("Q pressed");
        }
    }
}