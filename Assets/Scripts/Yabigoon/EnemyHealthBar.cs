using UnityEngine;
using UnityEngine.UI; // Slider를 사용하기 위해 추가

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider; // 슬라이더 컴포넌트

    // 체력 바 업데이트 함수
    public void SetHealth(float healthFraction)
    {
        // 체력 값을 0~1 사이의 비율로 받음
        slider.value = healthFraction;
    }

 
}