using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float lengthX, startposX;
    private float lengthY, startposY;
    private Transform cam;

    [Tooltip("The parallax effect strength on each axis. A value of 0 means the background doesn't move, 1 means it moves with the camera.")]
    public Vector2 parallaxEffect;

    void Start()
    {
        // Get the main camera
        cam = Camera.main.transform;

        // Store the starting position of the background
        startposX = transform.position.x;
        startposY = transform.position.y;

        // Get the size of the sprite. We use bounds.size which is the world-space size.
        if (GetComponent<SpriteRenderer>() != null)
        {
            lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
            lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
        }
        else
        {
            Debug.LogError("Parallax script requires a SpriteRenderer component.");
        }
    }

    void LateUpdate()
    {
        if (cam == null) return;

        // Calculate the distance the camera has moved, scaled by the parallax effect
        float distX = (cam.position.x * parallaxEffect.x);
        float distY = (cam.position.y * parallaxEffect.y);

        // Move the background layer
        transform.position = new Vector3(startposX + distX, startposY + distY, transform.position.z);

        // --- Horizontal Looping Logic ---
        // This checks how far the camera has moved relative to the parallax layer.
        float tempX = (cam.position.x * (1 - parallaxEffect.x));
        
        // If the camera has moved far enough to pass the edge of the sprite, we move the background's start position.
        if (tempX > startposX + lengthX)
        {
            startposX += lengthX;
        }
        else if (tempX < startposX - lengthX)
        {
            startposX -= lengthX;
        }

        // --- Vertical Looping Logic ---
        float tempY = (cam.position.y * (1 - parallaxEffect.y));

        if (tempY > startposY + lengthY)
        {
            startposY += lengthY;
        }
        else if (tempY < startposY - lengthY)
        {
            startposY -= lengthY;
        }
    }
}
