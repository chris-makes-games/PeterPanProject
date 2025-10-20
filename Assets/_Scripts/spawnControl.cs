using UnityEngine;
using UnityEngine.Rendering;

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
            float randomFairy = Random.Range(0, 100f);
            if (randomFairy < fairyDensity)
            {
                Instantiate(fairy, transform.position, Quaternion.identity); //makes fairy
            }
            
        }
        
    }
    //getters and setters for difficulty
    public void setTreeDensity(float density)
    {
        treeDensity = density;
    }

    public void setFairyDensity(float density)
    {
        fairyDensity = density;
    }

    public float getTreeDensity()
    {
        return treeDensity;
    }

    public float getFairyDensity()
    {
        return fairyDensity;
    }

    public float getInterval()
    {
        return treeTimer;
    }

    public void setInterval(float newInterval)
    {
        treeTimer = newInterval;
    }
}
