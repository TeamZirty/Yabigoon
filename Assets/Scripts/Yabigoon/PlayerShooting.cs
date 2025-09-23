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
    public GameObject timeGrenadePrefab; // Add this line
    public float grenadeThrowForce = 10f;

    [Header("Sound Effects")]
    public AudioClip switchSound;

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
        HandleGrenadeThrow(); // Handle grenade throwing input
        HandleTimeGrenadeThrow();
    }

    void HandleWeaponSwitching()
    {
        if (ownedWeapons.Count <= 1) return;

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
        if (Input.GetKeyDown(KeyCode.Alpha4))
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
                // Get mouse position in world coordinates
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0; // Ensure z-axis is 0 for 2D

                // Calculate direction from weaponHolder to mouse
                Vector2 throwDirection = (mousePos - weaponHolder.position).normalized;

                rb.AddForce(throwDirection * grenadeThrowForce, ForceMode2D.Impulse);
            }
        }
    }

    void HandleTimeGrenadeThrow()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (timeGrenadePrefab == null)
            {
                Debug.LogWarning("Time Grenade Prefab is not assigned in the Inspector!");
                return;
            }

            // Instantiate grenade at the weaponHolder's position
            GameObject grenade = Instantiate(timeGrenadePrefab, weaponHolder.position, Quaternion.identity);
            Rigidbody2D rb = grenade.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Get mouse position in world coordinates
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0; // Ensure z-axis is 0 for 2D

                // Calculate direction from weaponHolder to mouse
                Vector2 throwDirection = (mousePos - weaponHolder.position).normalized;

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
        currentWeapon = ownedWeapons[newIndex];
        currentWeapon.Equip();

        if (SoundManager.Instance != null && switchSound != null)
        {
            SoundManager.Instance.PlaySFX(switchSound);
        }

        nextAttackTime = Time.time;
    }
}
