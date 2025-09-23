using UnityEngine;

public class DistanceFade2D : MonoBehaviour
{
    // 플레이어의 Transform을 할당합니다.
    public Transform playerTransform;

    // 투명도가 0이 되는 (완전히 투명한) 최소 거리
    public float minDistance = 2f;

    // 투명도가 1이 되는 (완전히 불투명한) 최대 거리
    public float maxDistance = 10f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // 스크립트가 적용된 오브젝트의 SpriteRenderer 컴포넌트를 가져옵니다.
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 플레이어 Transform이 할당되었는지 확인합니다.
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform이 할당되지 않았습니다. 인스펙터 창에서 할당해주세요.");
            this.enabled = false;
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // 플레이어와 현재 오브젝트의 X축 위치 차이를 계산합니다.
            float distance = Mathf.Abs(playerTransform.position.x - transform.position.x);

            // 거리를 0과 1 사이의 값으로 변환합니다.
            float alpha = Mathf.InverseLerp(minDistance, maxDistance, distance);

            // 최종 알파 값을 0과 1 사이로 고정합니다.
            alpha = Mathf.Clamp01(alpha);

            // 현재 Sprite Renderer의 색상을 가져옵니다.
            Color newColor = spriteRenderer.color;

            // 계산된 알파 값을 색상에 적용합니다.
            newColor.a = alpha;

            // 변경된 색상을 Sprite Renderer에 다시 할당합니다.
            spriteRenderer.color = newColor;
        }
    }
}