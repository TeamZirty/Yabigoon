using UnityEngine;
using System.Collections.Generic;

public class PlayerShooting : MonoBehaviour
{
    [Header("Weapon Setup")]
    public List<GameObject> weaponPrefabs;
    public Transform weaponHolder;

    private List<WeaponBase> ownedWeapons = new List<WeaponBase>();
    private int currentWeaponIndex = -1;
    private WeaponBase currentWeapon;

    private float nextAttackTime = 0f;

    [Header("Grenade Setup")]
    public GameObject grenadePrefab;
    public float grenadeThrowForce = 10f;

    void Start()
    {
        foreach (GameObject weaponPrefab in weaponPrefabs)
        {
            GameObject weaponObj = Instantiate(weaponPrefab, weaponHolder.position, weaponHolder.rotation, weaponHolder);
            WeaponBase weapon = weaponObj.GetComponent<WeaponBase>();
            if (weapon != null)
            {
                ownedWeapons.Add(weapon);
                weapon.gameObject.SetActive(false); // Set inactive immediately after creation
            }
        }

        if (ownedWeapons.Count > 0)
        {
            SwitchWeapon(0); // Equip the first weapon (index 0) by default
        }
    }

    void Update()

    {
        HandleWeaponSwitching();
        HandleAttack();
        HandleGrenadeThrow(); // New: Handle grenade throwing input
    }

    void HandleWeaponSwitching()
    {
        if (ownedWeapons.Count <= 1) return;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            SwitchWeapon((currentWeaponIndex + 1) % ownedWeapons.Count);
        }
        else if (scroll < 0f)
        {
            int prevIndex = currentWeaponIndex - 1;
            if (prevIndex < 0) prevIndex = ownedWeapons.Count - 1;
            SwitchWeapon(prevIndex);
        }

        for (int i = 0; i < ownedWeapons.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SwitchWeapon(i);
                break;
            }
        }
    }

    void HandleAttack()

    {
        if (Input.GetMouseButton(0))
        {
            if (currentWeapon != null && Time.time >= nextAttackTime)
            {
                currentWeapon.Attack();
                
                if (currentWeapon.weaponData.fireRate > 0)
                {
                    nextAttackTime = Time.time + 1f / currentWeapon.weaponData.fireRate;
                }
            }
        }
    }

    void HandleGrenadeThrow()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (grenadePrefab == null)
            {
                Debug.LogWarning("Grenade Prefab is not assigned in the Inspector!");
                return;
            }

            // Instantiate grenade at the weaponHolder's position
            GameObject grenade = Instantiate(grenadePrefab, weaponHolder.position, Quaternion.identity);
            Rigidbody2D rb = grenade.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector2 throwDirection = weaponHolder.right;
                // Check player's facing direction (assuming weaponHolder's parent scale reflects player direction)
                if (weaponHolder.parent != null && weaponHolder.parent.localScale.x < 0)
                {
                    throwDirection = -throwDirection; // Flip direction if player is facing left
                }
                rb.AddForce(throwDirection * grenadeThrowForce, ForceMode2D.Impulse);
            }
        }
    }

    void SwitchWeapon(int newIndex)
    {
        if (newIndex < 0 || newIndex >= ownedWeapons.Count || newIndex == currentWeaponIndex)
        {
            return;
        }

        if (currentWeapon != null)
        {
            currentWeapon.Unequip();
        }

        currentWeaponIndex = newIndex;
        currentWeapon = ownedWeapons[currentWeaponIndex];
        currentWeapon.Equip();

        nextAttackTime = Time.time;
    }
}