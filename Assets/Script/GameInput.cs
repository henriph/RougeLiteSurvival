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
        playerControls.Enable();
        playerControls.Player.Move.performed += OnMovementPerformed;
        playerControls.Player.Move.canceled += OnMovementCancelled;
    }

    private void OnDisable()
    {
        playerControls.Player.Move.performed -= OnMovementPerformed;
        playerControls.Player.Move.canceled -= OnMovementCancelled;
        playerControls.Disable();
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
