using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [SerializeField] private InputActionAsset controls;
    private PlayerInput playerInput;
    private InputAction movementAction;
    private InputAction lookAction;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        playerInput = GetComponent<PlayerInput>();
        movementAction = playerInput.actions["Movement"];
        lookAction = playerInput.actions["Look"];
        
        DontDestroyOnLoad(this);
    }

    public Vector2 GetInputDirection()
    {
        return movementAction.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return lookAction.ReadValue<Vector2>();
    }

    public void SwitchCurrentMap(string actionMap)
    {
        playerInput.SwitchCurrentActionMap(actionMap);
    }
}
