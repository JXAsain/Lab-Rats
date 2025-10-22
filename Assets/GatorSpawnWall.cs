using UnityEngine;

public class GatorSpawnWall : MonoBehaviour
{
    [SerializeField] private GameObject gator; // Assign your Gator here in the Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gator != null)
        {
            gator.SetActive(true); // Enable the Gator so it starts moving
        }
    }
}
