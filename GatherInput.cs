using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{
    private Controls myControls;
    public float valueX;
    public bool jumpInput;
    public bool tryToAttack;

    private void Awake()
    {
        myControls = new Controls();
    }

    private void OnEnable()
    {
        myControls.Player.Move.performed += StartMove;
        myControls.Player.Move.canceled += StopMove;
        myControls.Player.Jump.performed += JumpStart;
        myControls.Player.Jump.canceled += JumpStop;
        myControls.Player.Attack.performed += TryToAttack;
        myControls.Player.Attack.canceled += StopTryToAttack;
        myControls.Player.Enable();
    }

    private void OnDisable()
    {
        myControls.Player.Move.performed -= StartMove;
        myControls.Player.Move.canceled -= StopMove;
        myControls.Player.Jump.performed -= JumpStart;
        myControls.Player.Jump.canceled -= JumpStop;
        myControls.Player.Attack.performed -= TryToAttack;
        myControls.Player.Attack.canceled -= StopTryToAttack;
        myControls.Player.Disable();
    }

    public void DisableControls()
    {
        myControls.Player.Move.performed -= StartMove;
        myControls.Player.Move.canceled -= StopMove;
        myControls.Player.Jump.performed -= JumpStart;
        myControls.Player.Jump.canceled -= JumpStop;
        myControls.Player.Attack.performed -= TryToAttack;
        myControls.Player.Attack.canceled -= StopTryToAttack;
        myControls.Player.Disable();
        valueX = 0;
    }

    private void StartMove(InputAction.CallbackContext ctx) { valueX = ctx.ReadValue<float>(); }
    private void StopMove(InputAction.CallbackContext ctx) { valueX = 0; }
    private void JumpStart(InputAction.CallbackContext ctx) { jumpInput = true; }
    private void JumpStop(InputAction.CallbackContext ctx) { jumpInput = false; }
    public void TryToAttack(InputAction.CallbackContext ctx) { tryToAttack = true; }
    public void StopTryToAttack(InputAction.CallbackContext ctx) { tryToAttack = false; }
}