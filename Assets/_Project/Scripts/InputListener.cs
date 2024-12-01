using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    private IA_Game inputActions;

    public event Action OnInteract;

    private void Awake()
    {
        inputActions = new();
        inputActions.Gameplay.Enable();
        inputActions.Gameplay.Interact.performed += Interact_performed;
    }

    private void OnDisable()
    {
        inputActions.Gameplay.Interact.performed -= Interact_performed;
        inputActions.Gameplay.Disable();
    }

    private void Interact_performed(InputAction.CallbackContext context)
    {
        OnInteract?.Invoke();
    }
}
