using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    public float gravity = 9.81f; // Gravity force

    public void Attract(Rigidbody rb, Transform planet)
    {
        Vector3 gravityDirection = (planet.position - rb.position).normalized;
        Vector3 localUp = rb.transform.up;

        // Apply gravity towards the planet's center
        rb.AddForce(gravityDirection * gravity);

        // Rotate the character to align with the planet's surface
        Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityDirection) * rb.rotation;
        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, 50f * Time.deltaTime);
    }
}
