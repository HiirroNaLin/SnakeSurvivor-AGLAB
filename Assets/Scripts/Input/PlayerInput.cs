using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Player Input")]
public class PlayerInput : ScriptableObject, InputActions.IKeyBoardGamePlayActions
{
    public event UnityAction<Single> onDirection = delegate {};

    public event UnityAction<Vector2> onRotate = delegate { };

    public event UnityAction onStopDirection = delegate {};

    public event UnityAction onStopRotate = delegate { };

    private InputActions inputActions;

    void OnEnable()
    {
        inputActions = new InputActions();

        inputActions.KeyBoardGamePlay.SetCallbacks(this);
    }

    void OnDisable()
    {
        DisableAllInput();
    }

    public void DisableAllInput()
    {
        inputActions.KeyBoardGamePlay.Disable();
    }

    public void EnableKeyBoardGamePlayInput()
    {
        inputActions.KeyBoardGamePlay.Enable();
    }

    public void OnDirection(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onDirection.Invoke(context.ReadValue<Single>());
        }
        if (context.phase == InputActionPhase.Canceled)
        {
           onStopDirection.Invoke();
        }

    }

    public void OnTest(InputAction.CallbackContext context)
    {
        
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onRotate.Invoke(context.ReadValue<Vector2>());
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            onStopRotate.Invoke();
        }
    }

    public void OnUI(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }
}
