using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private InputManager inputManager;

    void Start()
    {
        inputManager = InputManager.instance;
    }

    public void PauseMenu(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        inputManager.SwitchCurrentMap("Menu");
        pauseMenu.SetActive(true);
        Cursor.visible = true;
    }

    public void ResumeMenu(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        ResumeMenu();
    }

    public void ResumeMenu()
    {
        inputManager.SwitchCurrentMap("Game");
        pauseMenu.SetActive(false);
        Cursor.visible = false;
    }
}
