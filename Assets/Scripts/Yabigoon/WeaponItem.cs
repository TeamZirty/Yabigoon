using UnityEngine;

// 유니티 에디터에서 이 클래스를 사용해 새 에셋을 생성할 수 있게 해줍니다.
[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]
public class WeaponItem : ScriptableObject
{
    public string itemName; // 무기 이름
    public int price; // 구매 가격
    public int damage; // 공격력
    public float fireRate; // 연사 속도
    // 무기 아이콘, 특수 효과 등 다른 정보를 추가할 수 있습니다.
}