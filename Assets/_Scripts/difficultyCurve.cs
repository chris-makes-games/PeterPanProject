using UnityEngine;
using UnityEngine.Rendering;

public class difficultyCurve : MonoBehaviour
{
    //default speeds
    public float startTreeSpeed;
    public float startFairySpeed;
    public float startBackSpeedTree;
    public float startBackSpeedGround;
    public float startBackSpeedWater1;
    public float startBackSpeedWater2;

    //main controller for scaling difficulty
    public float difficulty;

    //spawns objects faster
    public GameObject spawner;

    //prefab objects
    public GameObject tree;
    public GameObject fairy;

    //backgrounds to scroll faster
    public GameObject treeBackgorund;
    public GameObject groundBackground;
    public GameObject waterBackground1;
    public GameObject waterBackground2;

    //private objects - scripts of above objects
    private treeScript treeControl;
    private spawnControl spawnControl;
    private fairyScript fairyControl;
    
    private BackgroundScroller trees;
    private BackgroundScroller ground;
    private BackgroundScroller water1;
    private BackgroundScroller water2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //game object scripts
        treeControl = tree.GetComponent<treeScript>();
        spawnControl = spawner.GetComponent<spawnControl>();
        fairyControl = fairy.gameObject.GetComponent<fairyScript>();

        //backgrounds
        trees = treeBackgorund.GetComponent<BackgroundScroller>();
        ground = groundBackground.GetComponent<BackgroundScroller>();
        water1 = waterBackground1.GetComponent<BackgroundScroller>();
        water2 = waterBackground2.GetComponent<BackgroundScroller>();

        //set starting speeds - reset at every game
        treeControl.setSpeed(startTreeSpeed);
        fairyControl.setSpeed(startFairySpeed);

        trees.setSpeed(startBackSpeedTree);
        ground.setSpeed(startBackSpeedGround);
        water1.setSpeed(startBackSpeedWater1);
        water2.setSpeed(startBackSpeedWater2);
    }

    // Update is called once per frame
    void Update()
    {
        trees.increaseSpeed(difficulty);
        ground.increaseSpeed(difficulty);
        water1.increaseSpeed(difficulty);
        water2.increaseSpeed(difficulty);

        treeControl.increaseSpeed(difficulty);
        fairyControl.increaseSpeed(difficulty);
    }
}
