using UnityEngine;

[CreateAssetMenu(fileName = "CharacterProfile", menuName = "Settings/Physics_Settings")]
public class PhysicsSettings : ScriptableObject
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float gravity = -12f;
}
