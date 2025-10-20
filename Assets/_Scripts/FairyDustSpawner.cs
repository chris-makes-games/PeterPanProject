using UnityEngine;
using System.Collections;

public class FairyDustSpawner : MonoBehaviour
{
    [Header("Fairy Dust Settings")]
    public GameObject fairyDustPrefab; // Reference to the Fairy Dust prefab
    public int maxFairies = 10;        // Limit on how many exist at once
    public float spawnAreaX = 8f;      // Horizontal range for spawning
    public float minY = 0f;            // Lower Y boundary
    public float maxY = 4f;            // Upper Y boundary

    private int currentFairies = 0;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            // Wait between 6~12 seconds before each spawn
            float waitTime = Random.Range(6f, 12f);
            yield return new WaitForSeconds(waitTime);

            if (currentFairies < maxFairies)
            {
                SpawnFairy();
            }
        }
    }

    void SpawnFairy()
    {
        // Random X and Y positions within defined range
        float randomX = Random.Range(-spawnAreaX, spawnAreaX);
        float randomY = Random.Range(minY, maxY);

        Vector3 spawnPos = new Vector3(randomX, randomY, 0f);

        GameObject fairy = Instantiate(fairyDustPrefab, spawnPos, Quaternion.identity);

        FairyDust fairyScript = fairy.GetComponent<FairyDust>();
        if (fairyScript != null)
        {
            fairyScript.Init(() => currentFairies--);
        }

        currentFairies++;
    }
}