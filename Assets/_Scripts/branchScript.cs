using System;
using UnityEngine;
using UnityEngine.Rendering;

public class branchScript : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.localPosition;
        float random_x = UnityEngine.Random.Range(1.0f, 10.0f);
        float random_y = UnityEngine.Random.Range(3.0f, 15.0f);
        endPos = new Vector2(random_x, random_y);
    }


}
