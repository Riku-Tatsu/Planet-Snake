using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Transform cameraPoint; // Reference to the camera point transform
    public float height = 10f; // Height above the player

    private void Start()
    {
        // Find the player and cameraPoint by tag if not assigned
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (cameraPoint == null && player != null)
        {
            cameraPoint = player.Find("CameraPoint");
        }
    }

    private void LateUpdate()
    {
        // If cameraPoint is not assigned, return
        if (cameraPoint == null)
            return;

        // Set the camera position to the cameraPoint position plus an offset for height
        transform.position = cameraPoint.position + cameraPoint.up * height;

        // Make the camera look at the player
        transform.LookAt(player);

        // Ensure the player's forward direction faces the top of the screen
        Vector3 direction = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, player.up);
        transform.rotation = rotation;
    }
}
