using UnityEngine;

public class WaveScroller : MonoBehaviour
{
    public float scrollSpeed = 2f; // Speed of scrolling
    private float waveWidth;       // Width of one wave image
    private Transform wave1;       // First wave
    private Transform wave2;       // Second wave

    void Start()
    {
        // Get references to both waves (the script should be placed on an empty parent)
        wave1 = transform.GetChild(0);
        wave2 = transform.GetChild(1);

        // Get the width of the sprite in world units
        SpriteRenderer sr = wave1.GetComponent<SpriteRenderer>();
        waveWidth = sr.bounds.size.x;
    }

    void Update()
    {
        // Move both waves to the left
        wave1.position += Vector3.left * scrollSpeed * Time.deltaTime;
        wave2.position += Vector3.left * scrollSpeed * Time.deltaTime;

        // If wave1 goes completely off screen, move it to the right of wave2
        if (wave1.position.x <= -waveWidth)
            wave1.position = new Vector3(wave2.position.x + waveWidth, wave1.position.y, wave1.position.z);

        // If wave2 goes completely off screen, move it to the right of wave1
        if (wave2.position.x <= -waveWidth)
            wave2.position = new Vector3(wave1.position.x + waveWidth, wave2.position.y, wave2.position.z);
    }
}