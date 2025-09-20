using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ShopManager.cs
    public void PurchaseWeapon(WeaponItem weapon)
    {
        if (PlayerStatus.Instance.currentGold >= weapon.price)
        {
            PlayerStatus.Instance.UseGold(weapon.price);
            // 무기 구매 로직: 플레이어에게 새로운 무기 장착 혹은 능력치 강화
            Debug.Log(weapon.itemName + "을(를) 구매했습니다.");
        }
        else
        {
            Debug.Log("골드가 부족합니다.");
        }
    }

}

