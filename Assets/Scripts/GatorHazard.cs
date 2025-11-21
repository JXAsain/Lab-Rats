using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GatorHazard : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;        // Speed of movement
    [SerializeField] private float moveDistance = 10f;    // How far Gator moves
    [SerializeField] private bool moveRight = true;       // Direction toggle

    [Header("Scene Settings")]
    [SerializeField] private int currentSceneID;          // ID of the current scene to reload

    private Vector3 startPos;
    private Vector3 endPos;
    private bool hasMoved = false;

    private void OnEnable()
    {
        // Cache positions when the Gator becomes active
        startPos = transform.position;
        Vector3 direction = moveRight ? Vector3.right : Vector3.left;
        endPos = startPos + direction * moveDistance;

        // Begin movement once enabled
        StartCoroutine(MoveOnce());
    }

    private IEnumerator MoveOnce()
    {
        if (hasMoved)
            yield break;

        hasMoved = true;
        yield return MoveToPosition(endPos, moveSpeed);
    }

    private IEnumerator MoveToPosition(Vector3 target, float speed)
    {
        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(currentSceneID, LoadSceneMode.Single);
        }
    }
}
