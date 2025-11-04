using UnityEngine;
using System.Collections;

public class creditScroll : MonoBehaviour
{
    private Transform selfTransform;
    public float scrollSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selfTransform = GetComponent<Transform>();
        StartCoroutine(GameOverWait());
    }

    // Update is called once per frame
    void Update()
    {
        selfTransform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
    }

    IEnumerator GameOverWait()
    {
        // Wait for game to end
        yield return new WaitForSeconds(30f);
        //close game
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
