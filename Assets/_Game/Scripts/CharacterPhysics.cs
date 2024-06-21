using UnityEngine;

public class CharacterPhysics : MonoBehaviour
{
    public PhysicsSettings physicsSettings;
    public Rigidbody playerRigidbody;
    public Transform planet;
    private float horizontalInput;

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
    }

    public void SetInput(float horizontal)
    {
        horizontalInput = horizontal;
    }
}