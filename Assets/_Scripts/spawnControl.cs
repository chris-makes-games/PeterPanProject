using UnityEngine;

public class spawnControl : MonoBehaviour
{
    //difficulty curve - use 50-100
    public float treeDensity = 50f;
    public float fairyDensity = 50f;

    //timer to wait between trees
    public float treeTimer = 2f;
    private float currentInterval;

    //define prefabs
    public GameObject tree;
    public GameObject fairy;

    void Start()
    {
        currentInterval = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentInterval += Time.deltaTime; //increments timer

        if (currentInterval > treeTimer) // if time passed greater than spawn timer
        {
            currentInterval = 0; //reset interval
            float randomTree = Random.Range(0, 100f);
            if (randomTree < treeDensity)
            {
                Instantiate(tree, transform.position, Quaternion.identity); //spawn tree if density high enough
            }
            else
            {
                Instantiate(fairy, transform.position, Quaternion.identity); //makes fairy where tree would be
            }
            
        }
        
    }
}
