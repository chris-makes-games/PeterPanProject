using UnityEngine;
using UnityEngine.SceneManagement;

public class spaceSkip : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        //check for pressing space and send player to appropriate next scene
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if (SceneManager.GetActiveScene().name == "Intro")
            {
                SceneManager.LoadScene("ChaseScene");
            }
            else if (SceneManager.GetActiveScene().name == "Middle")
            {
                SceneManager.LoadScene("BossScene");
            }
            else if (SceneManager.GetActiveScene().name == "End")
            {
                SceneManager.LoadScene("Credits");
            }
            else if (SceneManager.GetActiveScene().name == "EndSkip")
            {
                SceneManager.LoadScene("Credits");
            }
        }
    }
}
