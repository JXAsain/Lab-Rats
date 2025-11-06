using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowPlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player; // assign in inspector

    [Header("Sizes")]
    [SerializeField] private float normalSize = 8f;
    [SerializeField] private float zoomedSize = 15f;
    [SerializeField] private float sizeLerpSpeed = 4f;

    [Header("Y positions (world Y)")]
    [SerializeField] private float normalY = 0f;       // camera Y when not zoomed
    [SerializeField] private float zoomedY = 15.72f;   // camera Y when zoomed
    [SerializeField] private float yLerpSpeed = 4f;

    private Camera cam;
    private bool isZoomed = false;
    private float targetSize;
    private float targetY;

    private void Awake()
    {
        cam = GetComponent<Camera>();

        // initialize targets to starting values
        targetSize = normalSize;
        targetY = normalY;

        // (optional) enforce starting values
        cam.orthographicSize = normalSize;
        Vector3 startPos = transform.position;
        startPos.y = normalY;
        transform.position = startPos;
    }

    private void Update()
    {
        if (player == null)
        {
            // Try to find again if missing
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer != null)
                player = foundPlayer.transform;
            else
                return; // skip update until player exists
        }
        // 1) Follow player's X only
        Vector3 pos = transform.position;
        pos.x = player.position.x;
        transform.position = pos;

        // 2) Smoothly interpolate orthographic size
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, Time.deltaTime * sizeLerpSpeed);

        // 3) Smoothly interpolate Y position to targetY (world Y)
        Vector3 cur = transform.position;
        cur.y = Mathf.Lerp(cur.y, targetY, Time.deltaTime * yLerpSpeed);
        transform.position = cur;
    }

    // Toggle zoom / position state (call from trigger)
    public void ToggleZoom()
    {
        isZoomed = !isZoomed;
        targetSize = isZoomed ? zoomedSize : normalSize;
        targetY = isZoomed ? zoomedY : normalY;
    }

    // Optional: explicit set methods if you prefer OnEnter/OnExit behavior
    public void SetZoomed(bool zoomed)
    {
        isZoomed = zoomed;
        targetSize = isZoomed ? zoomedSize : normalSize;
        targetY = isZoomed ? zoomedY : normalY;
    }
}
