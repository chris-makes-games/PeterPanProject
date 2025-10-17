using UnityEngine;


public class treeScript : MonoBehaviour
{
    //how fast tree moves to left
    public float treeSpeed;

    //branch difficulty curve
    //use between 0 and 100
    public float branchDensity = 50f;
    public int maxBranches = 3;
    private int currentBranches;

    //min and max for branch placement
    public float lowestBranch = 0f;
    public float highestBranch = 0f;

    //min and max brnch length
    public float maxBranchX;
    public float maxBranchY;

    //prefabs for branches and leaves
    public GameObject branch;
    public GameObject leaves;

    //where branch starts and stops
    private Vector2 startBranch;
    private Vector2 endBranch;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //starts at zero branches
        currentBranches = 0;

        //always spawns at least one branch
        generateBranch();

        //chance to spawn more branches depending on density
        //tries each time for maxBranches, might spawn every time
        while (currentBranches < maxBranches)
        {
            float randomChance = Random.Range(0, 100f);
            if (randomChance < branchDensity)
            {
                generateBranch();
            }
            currentBranches++;
        }
    }

    void Update()
    {
        //move tree and branch/leave children to the left at speed
        transform.position += Time.deltaTime * treeSpeed * Vector3.left;
    }

    void generateBranch()
    {
        //choose spot on trunk to begin branch
        float startRandom_y = UnityEngine.Random.Range(lowestBranch, highestBranch);
        startBranch = new Vector2(transform.position.x, transform.position.y + startRandom_y);

        //choose spot to end branch
        //allows x to be negative and go left, but branches always go upwards
        float endRandom_x = UnityEngine.Random.Range(-maxBranchX - 1, maxBranchX + 1);
        float endRandom_y = UnityEngine.Random.Range(0, maxBranchY);
        endBranch = new Vector2(startBranch.x + endRandom_x, startBranch.y + endRandom_y);

        //Calculate direction and distance between start and end branch
        Vector2 direction = endBranch - startBranch;
        float length = direction.magnitude;
        Vector2 midpoint = startBranch + direction / 2f;

        //Instantiate the branch at the midpoint
        GameObject newBranch = Instantiate(branch, midpoint, Quaternion.identity, this.transform);

        //Create angle usung Mathf to get degree of angle between two points
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform branch to match the angle of roation between tree and leaves
        newBranch.transform.rotation = Quaternion.Euler(0, 0, angle);

        //Stretch the branch to match the distance between tree and leaves
        newBranch.transform.localScale = new Vector3(length, newBranch.transform.localScale.y, 1f);

        //place leaves at endpoint of branches
        GameObject newLeaves = Instantiate(leaves, endBranch, Quaternion.identity, this.transform);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TreeDestroy"))
        {
            Destroy(gameObject);
        }
    }




}


