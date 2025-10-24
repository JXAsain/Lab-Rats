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
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(currentSceneID, LoadSceneMode.Single);
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
