using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    public static GameInput Instance { get; private set; }

    private PlayerInputActions playerInputActions;


    private void OnEnable()
    {
        playerInputActions.Enable();
    }


    private void OnDisable()
    {
        playerInputActions.Disable();
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerInputActions = new PlayerInputActions();

        playerInputActions.Player.Pause.performed += Pause_performed;
    }


    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Actions.OnPauseButtonPressed?.Invoke();
    }

}
