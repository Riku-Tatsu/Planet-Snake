using UnityEngine;

public class InputManager : MonoBehaviour
{
    public CharacterPhysics characterPhysics;
    public SnakeGrow snakeGrow;
    public bool drawDebugLines = true; // Toggle for debug lines

    private Vector3 inputDirection;

    public void ReceiveInput(float horizontalInput)
    {
        inputDirection = new Vector3(horizontalInput, 0, 1); // Constant forward direction
        characterPhysics.SetInput(horizontalInput);
    }

    public void ProcessQKeyPress()
    {
        if (snakeGrow != null)
        {
            snakeGrow.OnGrow();
        }
    }

    public float GetHorizontalInput()
    {
        return inputDirection.x;
    }

    public float GetVerticalInput()
    {
        return inputDirection.z;
    }

    private void OnDrawGizmos()
    {
        if (drawDebugLines)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(characterPhysics.transform.position, characterPhysics.transform.position + inputDirection);
        }
    }
}
