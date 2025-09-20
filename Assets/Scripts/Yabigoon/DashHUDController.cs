using UnityEngine;
using UnityEngine.UI; // UI 사용을 위해 추가

public class DashHUDController : MonoBehaviour
{
    public Image cooldownImage; // 쿨타임 이미지를 연결할 변수 (원형/방사형 채우기 이미지)
    public Image dashIconImage; // 대쉬 아이콘 이미지 (쿨타임 이미지 위에 표시될 아이콘)

    // Start is called before the first frame update
    void Start()
    {
        // 초기에는 쿨타임 이미지가 쿨타임 없음 상태 (0f)로 보이고, 아이콘은 활성화
        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = 0f;
        }
        if (dashIconImage != null)
        {
            // dashIconImage.color = Color.white; // 쿨타임 아닐 때 밝은 상태
        }
    }

    // PlayerController에서 호출하여 쿨타임 상태 업데이트
    // fillAmount: 0 (쿨타임 끝) ~ 1 (쿨타임 시작)
    public void UpdateCooldown(float fillAmount)
    {
        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = fillAmount; // 쿨타임 이미지를 채움
        }

        // 아이콘의 색상으로 쿨타임 상태를 표현할 수도 있음
        // if (dashIconImage != null)
        // {
        //     // 쿨타임 중이면 어둡게, 쿨타임이 끝나면 밝게 (선택 사항)
        //     dashIconImage.color = Color.Lerp(Color.white, Color.gray, fillAmount); 
        // }
    }
}