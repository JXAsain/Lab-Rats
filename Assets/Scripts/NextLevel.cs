using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [Header("Next Level Index Below")]
    public int sceneBuildIndex;

    [Header("Etc.")]
    public Animator transition;
    public float transitionTime = 1f;
    public int currentSceneID;


    // Level move zoned enter, if collider is a player
    // Move game to another scene
    private void Update()
    {
        //Restart level
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(currentSceneID, LoadSceneMode.Single);
        }

        // Move to main menu
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
        // Move to level 1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }

        // Move to level 2
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }

        // Move to level 3
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(3, LoadSceneMode.Single);
        }

        // Move to level 4
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SceneManager.LoadScene(4, LoadSceneMode.Single);
        }

        // Move to level 5
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SceneManager.LoadScene(5, LoadSceneMode.Single);
        }

        // Move to level 6
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SceneManager.LoadScene(6, LoadSceneMode.Single);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger Entered");

        if(other.tag == "Player")
        {
            // Player entered, so move level
            // Debug.Log("Switching Scene from " + SceneManager.GetActiveScene().name);

            StartCoroutine(LoadLevel(sceneBuildIndex));

        }

        IEnumerator LoadLevel(int sceneBuildIndex)
        {
            transition.SetTrigger("Start");

            yield return new WaitForSeconds(transitionTime);

            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }

    }
}
