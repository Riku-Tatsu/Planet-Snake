using UnityEngine;
using System.Collections.Generic;

public class CharacterPhysics : MonoBehaviour
{
    public PhysicsSettings physicsSettings;
    public Rigidbody playerRigidbody;
    public Transform planet;
    public GameObject bodyPrefab;
    public float gap = 0.5f;
    public bool drawPath = true; // Toggle for drawing the path
    public float pathFadeDistance = 10f; // Distance after which the path fades

    private float horizontalInput;
    private List<Transform> bodyParts = new List<Transform>();
    private List<Vector3> positions = new List<Vector3>();

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.useGravity = false;
        playerRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        MoveCharacter();
        MoveBodyParts();
    }

    private void ApplyGravity()
    {
        Vector3 gravityUp = (transform.position - planet.position).normalized;
        Vector3 localUp = transform.up;

        playerRigidbody.AddForce(gravityUp * physicsSettings.gravity);

        Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50f * Time.deltaTime);
    }

    private void MoveCharacter()
    {
        // Apply constant forward movement
        Vector3 moveDirection = transform.forward * physicsSettings.moveSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + moveDirection);

        // Apply rotation based on horizontal input
        if (horizontalInput != 0)
        {
            Quaternion rotation = Quaternion.Euler(0, horizontalInput * physicsSettings.rotationSpeed * Time.deltaTime, 0);
            transform.rotation *= rotation;
        }

        // Track the head's position
        positions.Insert(0, transform.position);
        if (positions.Count > (bodyParts.Count + 1) * (int)(gap / (physicsSettings.moveSpeed * Time.deltaTime)))
        {
            positions.RemoveAt(positions.Count - 1);
        }
    }

    public void SetInput(float horizontal)
    {
        horizontalInput = horizontal;
    }

    public void GrowSnake()
    {
        Vector3 spawnPosition = positions.Count > 0 ? positions[positions.Count - 1] : transform.position - transform.forward * gap;
        GameObject bodyPart = Instantiate(bodyPrefab, spawnPosition, Quaternion.identity);
        bodyParts.Add(bodyPart.transform);
    }

    private void MoveBodyParts()
    {
        for (int i = 0; i < bodyParts.Count; i++)
        {
            Vector3 newPos = positions[Mathf.Min((i + 1) * (int)(gap / (physicsSettings.moveSpeed * Time.deltaTime)), positions.Count - 1)];
            Vector3 direction = newPos - bodyParts[i].position;
            bodyParts[i].position = newPos;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, transform.up);
                bodyParts[i].rotation = Quaternion.Slerp(bodyParts[i].rotation, targetRotation, 50f * Time.deltaTime);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (drawPath)
        {
            Gizmos.color = Color.green;
            float totalDistance = 0f;

            for (int i = 0; i < positions.Count - 1; i++)
            {
                float segmentDistance = Vector3.Distance(positions[i], positions[i + 1]);
                totalDistance += segmentDistance;

                if (totalDistance > pathFadeDistance)
                {
                    break;
                }

                Gizmos.DrawLine(positions[i], positions[i + 1]);
            }
        }
    }
}
