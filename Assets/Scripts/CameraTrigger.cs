using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [Tooltip("Assign the camera's FollowPlayer script here. If left empty, it will try Camera.main.")]
    public FollowPlayer followPlayerScript;

    [Tooltip("Which tag counts as the player?")]
    public string playerTag = "Player";

    private void Awake()
    {
        if (followPlayerScript == null && Camera.main != null)
        {
            followPlayerScript = Camera.main.GetComponent<FollowPlayer>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;
        if (followPlayerScript != null)
        {
            followPlayerScript.ToggleZoom(); // toggles on each crossing
        }
    }
}
