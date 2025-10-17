using UnityEngine;
//copy-pasting from flappykaboom class repo

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 2f; // Adjust this to control background scroll speed
    public float backgroundWidth; // The width of a single background tile

    private Transform[] backgrounds; // Array to hold references to your two background tiles

    void Start()
    {
        // Get references to the child background tiles
        backgrounds = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            backgrounds[i] = transform.GetChild(i);
        }

        // Calculate the width of a single background tile
        // Assuming both tiles have the same width and a SpriteRenderer
        if (backgrounds.Length > 0 && backgrounds[0].GetComponent<SpriteRenderer>() != null)
        {
            backgroundWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
        }
        else
        {
            Debug.LogError("Background tiles or SpriteRenderer not found!");
        }
    }

    void Update()
    {
        // Move all background tiles to the left
        foreach (Transform bg in backgrounds)
        {
            bg.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        }

        // Check if the first background tile is off-screen and reposition it
        if (backgrounds[0].position.x < -backgroundWidth)
        {
            // Move the first tile to the right of the third tile (changes to three tiles)
            backgrounds[0].position = new Vector3(backgrounds[1].position.x + backgroundWidth * 2, backgrounds[0].position.y, backgrounds[0].position.z);
            // Swap the order in the array to maintain first/second/third tiles
            Transform temp = backgrounds[0];
            backgrounds[0] = backgrounds[1];
            backgrounds[1] = backgrounds[2];
            backgrounds[2] = temp;
        }
    }
}