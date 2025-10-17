using UnityEngine;

public class treeSpawner : MonoBehaviour
{
    //difficulty curve - use 50-100
    public float treeDensity = 50f;

    //timer to wait between trees
    public float spawnTimer = 2f;
    private float currentInterval;

    //tree object prefab
    public GameObject tree;

    void Start()
    {
        currentInterval = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentInterval += Time.deltaTime; //increments timer

        if (currentInterval > spawnTimer) // if time passed greater than spawn timer
        {
            currentInterval = 0; //reset interval
            float randomTree = Random.Range(0, 100f);
            if (randomTree < treeDensity)
            {
                Instantiate(tree, transform.position, Quaternion.identity); //spawn tree if density high enough
            }
            
        }
        
    }
}
