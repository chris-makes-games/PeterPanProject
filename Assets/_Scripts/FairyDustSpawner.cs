using UnityEngine;
using System.Collections;

public class FairyDustSpawner : MonoBehaviour
{
    public GameObject fairyDustPrefab;
    public Vector2 spawnAreaMin = new Vector2(-8f, -4f);
    public Vector2 spawnAreaMax = new Vector2(8f, 4f);
    public float spawnInterval = 30f; // interval between spawns
    public float dustLifetime = 15f;  // how long it stays before auto-destroy

    private GameObject currentDust;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (currentDust == null)
            {
                SpawnOne();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnOne()
    {
        Vector2 pos = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        currentDust = Instantiate(fairyDustPrefab, pos, Quaternion.identity);
        FairyDustItem dustScript = currentDust.GetComponent<FairyDustItem>();
        if (dustScript != null)
        {
            dustScript.spawner = this;  // so it can tell spawner when collected
        }

        Destroy(currentDust, dustLifetime);
    }

    public void NotifyCollected()
    {
        currentDust = null; // allows new dust to spawn next time
    }
}