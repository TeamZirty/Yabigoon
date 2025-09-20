using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static PlayerStatus Instance;

    public int currentGold; // 현재 보유 골드
    // 여기에 플레이어 체력, 공격력 등 다른 스탯 변수들을 추가할 수 있습니다.

    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 파괴되지 않게 설정
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 골드 추가 함수
    public void AddGold(int amount)
    {
        currentGold += amount;
        Debug.Log("골드 획득! 현재 골드: " + currentGold);
        // UI 업데이트 로직을 여기에 추가할 수 있습니다.
    }

    // 골드 사용 함수
    public void UseGold(int amount)
    {
        currentGold -= amount;
        Debug.Log("골드 사용. 현재 골드: " + currentGold);
        // UI 업데이트 로직을 여기에 추가할 수 있습니다.
    }
}