using System.Collections.Generic;
using UnityEngine;

public class SnakeGrow : MonoBehaviour
{
    // Public reference to the armature object
    public Transform armatureOrigin;
    // Public reference to the body segment prefab
    public GameObject bodySegmentPrefab;
    // Public spacing value for the body segments
    public float segmentSpacing = 1.0f;

    // List to store the position history of the origin point
    private List<Vector3> positionHistory = new List<Vector3>();
    // List to store the body segments
    private List<GameObject> bodySegments = new List<GameObject>();

    // Public method to be called when the snake grows
    public void OnGrow()
    {
        // Spawn a new body segment
        if (bodySegmentPrefab != null && positionHistory.Count > 0)
        {
            Vector3 spawnPosition;
            Quaternion spawnRotation;

            if (bodySegments.Count == 0)
            {
                // Spawn the first segment directly behind the head
                spawnPosition = armatureOrigin.position - armatureOrigin.forward * segmentSpacing;
                spawnRotation = armatureOrigin.rotation;
            }
            else
            {
                // Spawn subsequent segments at the last position in the history
                int historyIndex = Mathf.Clamp((bodySegments.Count + 1) * (int)segmentSpacing, 0, positionHistory.Count - 1);
                spawnPosition = positionHistory[historyIndex];
                spawnRotation = bodySegments[bodySegments.Count - 1].transform.rotation;
            }

            GameObject newSegment = Instantiate(bodySegmentPrefab, spawnPosition, spawnRotation);
            bodySegments.Add(newSegment);
        }
    }

    private void FixedUpdate()
    {
        // Track the position of the origin point
        if (armatureOrigin != null)
        {
            positionHistory.Insert(0, armatureOrigin.position);
            // Keep the position history to match the number of body segments plus the head
            int requiredHistoryLength = (bodySegments.Count + 1) * (int)segmentSpacing + 1;
            if (positionHistory.Count > requiredHistoryLength)
            {
                positionHistory.RemoveAt(positionHistory.Count - 1);
            }
        }

        // Update the position and rotation of the body segments
        for (int i = 0; i < bodySegments.Count; i++)
        {
            int historyIndex = Mathf.Clamp((i + 1) * (int)segmentSpacing, 0, positionHistory.Count - 1);
            Vector3 newPosition = positionHistory[historyIndex];
            Vector3 direction = newPosition - bodySegments[i].transform.position;
            
            if (direction != Vector3.zero)
            {
                Quaternion newRotation = Quaternion.LookRotation(direction);
                bodySegments[i].transform.rotation = newRotation;
            }

            bodySegments[i].transform.position = newPosition;
        }
    }

    private void OnDrawGizmos()
    {
        // Draw the path visually using Gizmos
        if (positionHistory.Count > 1)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < positionHistory.Count - 1; i++)
            {
                Gizmos.DrawLine(positionHistory[i], positionHistory[i + 1]);
            }
        }
    }
}
