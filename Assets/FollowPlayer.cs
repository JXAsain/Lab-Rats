using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;

    void Update()
    {
        Vector3 cameraPosition = transform.position; // current camera position
        cameraPosition.x = player.transform.position.x; // only update X
        transform.position = cameraPosition; // apply new position
    }
}
