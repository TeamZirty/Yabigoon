using UnityEngine;

// This is the base class for all weapons in the game.
public abstract class WeaponBase : MonoBehaviour
{
    // A link to the ScriptableObject that holds the weapon's stats.
    public WeaponItem weaponData;

    // We protect it so child classes like Gun.cs can access it if needed.
    protected SpriteRenderer spriteRenderer;

    // Awake is called when the script instance is being loaded.
    protected virtual void Awake()
    {
        // Find the SpriteRenderer on this weapon's GameObject.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // The primary action of the weapon (shooting, swinging, etc.).
    public abstract void Attack();

    // Called when the player equips this weapon.
    public virtual void Equip()
    {
        gameObject.SetActive(true);

        // Update the weapon's sprite to match the one in its data file.
        if (spriteRenderer != null && weaponData != null && weaponData.weaponSprite != null)
        {
            spriteRenderer.sprite = weaponData.weaponSprite;
        }
    }

    // Called when the player unequips this weapon.
    public virtual void Unequip()
    {
        gameObject.SetActive(false);
    }
}