using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class VerticalHazard : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float dropSpeed = 20f;    // Speed of downward strike
    [SerializeField] private float pauseTime = 1f;     // How long it waits at the bottom
    [SerializeField] private float dropDistance = 5f;  // How far it drops

    private Vector3 startPos;
    private Vector3 endPos;

    [Header("Current Scene")]
    //[SerializeField] private Transform playerSpawn; // Drag spawn point here
    [SerializeField] private int currentSceneID;
    private void Start()
    {
        startPos = transform.position;
        endPos = startPos + Vector3.down * dropDistance;

        StartCoroutine(LaserRoutine());
    }

    private IEnumerator LaserRoutine()
    {
        while (true)
        {
            // 1. Drop down fast
            yield return MoveToPosition(endPos, dropSpeed);

            // 2. Pause at bottom
            yield return new WaitForSeconds(pauseTime);

            // 3. Instantly reset back to start
            transform.position = startPos;

            // Optional: pause a bit before firing again
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator MoveToPosition(Vector3 target, float speed)
    {
        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //// Reset player to spawn
            //collision.transform.position = playerSpawn.position;

            //// Reset velocity so player doesn't keep falling
            //Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            //if (rb != null)
            //    rb.linearVelocity = Vector2.zero;

            SceneManager.LoadScene(currentSceneID, LoadSceneMode.Single);


        }
    }
}