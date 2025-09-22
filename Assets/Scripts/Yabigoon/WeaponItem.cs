using UnityEngine;

// This attribute allows you to create and manage instances of this class as assets in the Unity Editor.
[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]
public class WeaponItem : ScriptableObject
{
    public string itemName;     // The name of the item
    public int price;          // The price of the item
    public int damage;         // The weapon's damage
    public float fireRate;       // The weapon's rate of fire
    public Sprite weaponSprite; // The sprite that represents this weapon
    // You can add other properties here, like weapon type, special effects, etc.
}
