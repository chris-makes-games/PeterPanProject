using UnityEngine;

public class fairyBar : MonoBehaviour
{
    public GameObject fairyTracker;
    private difficultyCurve trackerScript;
    private int fairiesNeeded;
    private float moveIncrement;
    private float totalDistance = 16f; //distance the mask is from being aligned
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trackerScript = fairyTracker.GetComponent<difficultyCurve>();
        fairiesNeeded = trackerScript.fairiesNeeded; // gets total fairies needed
        moveIncrement = totalDistance / fairiesNeeded; ; //fraction of total distance to move
        
    }

    public void MoveBarUp()
    {
        Vector2 currentPosition = transform.position;
        transform.position = currentPosition + new Vector2(moveIncrement, 0); //moves by amount
    }
}
