using UnityEngine;

public class Shockwave : MonoBehaviour
{
    [Tooltip("The maximum size the shockwave will expand to.")]
    public float maxSize = 10f;
    [Tooltip("The time it takes for the shockwave to reach its max size and fade out.")]
    public float duration = 1f;

    private SpriteRenderer spriteRenderer;
    private float timer = 0f;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("Shockwave script requires a SpriteRenderer component.");
            enabled = false; // Disable the script if no renderer is found
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Calculate how far along we are in the animation (a value from 0 to 1)
        float progress = timer / duration;

        // Use Lerp to smoothly expand the scale from 0 to maxSize
        float currentSize = Mathf.Lerp(0f, maxSize, progress);
        transform.localScale = new Vector3(currentSize, currentSize, 1f);

        // Use Lerp to smoothly fade the alpha from 1 (opaque) to 0 (transparent)
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(1f, 0f, progress);
            spriteRenderer.color = color;
        }

        // Destroy the shockwave object once the duration is over
        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }
}
