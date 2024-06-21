using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public InputManager inputManager;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        inputManager.ReceiveInput(horizontalInput);

        // Process Q key press
        if (Input.GetKeyDown(KeyCode.Q))
        {
            inputManager.ProcessQKeyPress();
        }
    }
}
