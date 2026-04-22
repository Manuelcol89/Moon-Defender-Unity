using UnityEngine;

public class MissionUI : MonoBehaviour
{

    private PlayerInputActions m_actions;


    private void OnEnable()
    {
        m_actions.Player.Enable();
    }


    private void OnDisable()
    {
        m_actions.Player.Disable();
    }


    private void Awake()
    {
        m_actions = new PlayerInputActions();
        m_actions.Player.Shoot.performed += HideMissionUI;
    }


    private void Start()
    {
        ShowMissionUI();
    }


    private void ShowMissionUI()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }


    private void HideMissionUI(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

}
