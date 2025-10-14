using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerControls playerControls;
    private Vector2 inputMovement;

    public Vector2 InputMovementNormalized {  get { return inputMovement.normalized; } }
    
    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        if (playerControls != null)
        {
            playerControls.Enable();
            playerControls.Player.Move.performed += OnMovementPerformed;
            playerControls.Player.Move.canceled += OnMovementCancelled;
        }
    }

    private void OnDisable()
    {
        if (playerControls != null)
        {
            playerControls.Player.Move.performed -= OnMovementPerformed;
            playerControls.Player.Move.canceled -= OnMovementCancelled;
            playerControls.Disable();
        }
    }

    private void OnMovementPerformed(InputAction.CallbackContext obj)
    {
        inputMovement = obj.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext obj)
    {
        inputMovement = Vector2.zero;
    }

}
