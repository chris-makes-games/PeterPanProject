using UnityEngine;
using UnityEngine.SceneManagement;

public class difficultyCurve : MonoBehaviour
{
    //fairies to collect until next scene
    public int fairiesNeeded;

    //defaults - resets every game
    public float startTreeSpeed;
    public float startFairySpeed;
    public float startBackSpeedTree;
    public float startBackSpeedGround;
    public float startBackSpeedWater1;
    public float startBackSpeedWater2;
    public float startTreeDensity;
    public float startFairyDensity;
    public float startInterval;


    //controllers for scaling difficulty
    public float difficulty;
    public float forgivenessCurve;
    public float punishmentCurve;

    //spawns objects faster
    public GameObject spawner;

    //prefab objects
    public GameObject tree;
    public GameObject fairy;

    //player object for health
    public GameObject player;
    public peterFly playerScript;

    //backgrounds to scroll faster
    public GameObject treeBackgorund;
    public GameObject groundBackground;
    public GameObject waterBackground1;
    public GameObject waterBackground2;

    //private objects - scripts of above objects
    private treeScript treeControl;
    private spawnControl spawnControl;
    private fairyScript fairyControl;

    //controls the fairy dust meter
    public GameObject fairyBar;
    public fairyBar fairyBarScript;
    
    //gameobjects for background scroll speeds
    private BackgroundScroller trees;
    private BackgroundScroller ground;
    private BackgroundScroller water1;
    private BackgroundScroller water2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //player object health for tracking perfect run
        playerScript = player.GetComponent<peterFly>();

        //fairy bar script to move mask
        fairyBarScript = fairyBar.GetComponent<fairyBar>();

        //gameobject controller for density and timing
        spawnControl = spawner.GetComponent<spawnControl>();

        //game object scripts for basic speed
        treeControl = tree.GetComponent<treeScript>();
        fairyControl = fairy.gameObject.GetComponent<fairyScript>();

        //backgrounds
        trees = treeBackgorund.GetComponent<BackgroundScroller>();
        ground = groundBackground.GetComponent<BackgroundScroller>();
        water1 = waterBackground1.GetComponent<BackgroundScroller>();
        water2 = waterBackground2.GetComponent<BackgroundScroller>();

        //set starting speeds - reset at every game
        trees.setSpeed(startBackSpeedTree);
        ground.setSpeed(startBackSpeedGround);
        water1.setSpeed(startBackSpeedWater1);
        water2.setSpeed(startBackSpeedWater2);

        treeControl.setSpeed(startTreeSpeed);
        fairyControl.setSpeed(startFairySpeed);

        spawnControl.setTreeDensity(startTreeDensity);
        spawnControl.setFairyDensity(startFairyDensity);
        spawnControl.setInterval(startInterval);
    }

    // Update is called once per frame
    void Update()
    {
        //scrolling speed for backgrounds
        trees.increaseSpeed(difficulty * Time.deltaTime);
        ground.increaseSpeed(difficulty * Time.deltaTime);
        water1.increaseSpeed(difficulty * Time.deltaTime);
        water2.increaseSpeed(difficulty * Time.deltaTime);

        //movement speed for trees/fairies
        treeControl.increaseSpeed(difficulty * Time.deltaTime);
        fairyControl.increaseSpeed(difficulty * Time.deltaTime);

        //tree density
        spawnControl.setTreeDensity(spawnControl.getTreeDensity() + difficulty * Time.deltaTime); 
        spawnControl.setInterval(spawnControl.getInterval() - difficulty * Time.deltaTime * 0.01f); //shorter interval, very small decrement
        //fairy density unchanged - don't want lots of them
        
    }

    public void fairyCollected()
    {
        fairyBarScript.MoveBarUp();
        int currentHealth = playerScript.getPlayerHealth(); //gets current health from player before check
        fairiesNeeded--;
        if (currentHealth == 5 && fairiesNeeded <= 0) //collected all the fairies AND no hits taken
        {
            SceneManager.LoadScene("EndSkip");//skips the boss fight, secret ending
        }
        else if (fairiesNeeded <= 0) //collected all fairies
        {
            SceneManager.LoadScene("Middle");//goes to boss scene title card
        }
        }

    public void increaseDifficulty() //increases difficulty by fixed amount
    {
        trees.setSpeed(trees.getSpeed() + punishmentCurve);
        ground.setSpeed(ground.getSpeed() + punishmentCurve);
        water1.setSpeed(water1.getSpeed() + punishmentCurve);
        water2.setSpeed(water2.getSpeed() + punishmentCurve);

        treeControl.setSpeed(treeControl.getSpeed() + punishmentCurve);
        fairyControl.setSpeed(fairyControl.getSpeed() + punishmentCurve);

        spawnControl.setTreeDensity(spawnControl.getTreeDensity() + punishmentCurve);
        spawnControl.setInterval(spawnControl.getInterval() - punishmentCurve * 0.1f);
    }

    public void decreaseDifficulty() //decreases difficulty by fixed amount
    {
        trees.setSpeed(trees.getSpeed() - forgivenessCurve);
        ground.setSpeed(ground.getSpeed() - forgivenessCurve);
        water1.setSpeed(water1.getSpeed() - forgivenessCurve);
        water2.setSpeed(water2.getSpeed() - forgivenessCurve);

        treeControl.setSpeed(treeControl.getSpeed() - forgivenessCurve);
        fairyControl.setSpeed(fairyControl.getSpeed() - forgivenessCurve);

        spawnControl.setTreeDensity(spawnControl.getTreeDensity() - forgivenessCurve);
        //increase these to lower difficulty:
        spawnControl.setInterval(spawnControl.getInterval() + forgivenessCurve * 0.1f);
        spawnControl.setFairyDensity(spawnControl.getFairyDensity() + forgivenessCurve);
    }
}
