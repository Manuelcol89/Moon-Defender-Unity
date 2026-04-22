using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PausedUI : MonoBehaviour
{

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private CanvasGroup canvasGroupPausedUI;

    private Tween fadeTween;


    private void Start()
    {        
        pauseButton.onClick.AddListener(() =>
        {
            GameManager.Instance.PauseGame();
        });

        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.UnpauseGame();
        });

        Hide();
    }


    private void OnEnable()
    {
        Actions.OnGamePaused += Actions_OnGamePaused;
        Actions.OnGameUnpaused += Actions_OnGameUnpaused;
    }


    private void OnDisable()
    {
        Actions.OnGamePaused -= Actions_OnGamePaused;
        Actions.OnGameUnpaused -= Actions_OnGameUnpaused;

    }


    private void Actions_OnGamePaused()
    {
        Show();
    }


    private void Actions_OnGameUnpaused()
    {
        Hide();
    }


    private void Show()
    {
        canvasGroupPausedUI.alpha = 0;

        float fadeTime = 1f;
        fadeTween = canvasGroupPausedUI.DOFade(1, fadeTime).SetUpdate(true).OnComplete(() =>
        {
            canvasGroupPausedUI.interactable = true;
            canvasGroupPausedUI.blocksRaycasts = true;
        });
    }


    private void Hide()
    {
        if (fadeTween != null && fadeTween.IsActive())
        {
            fadeTween.Kill();
        }

        canvasGroupPausedUI.alpha = 0;
        canvasGroupPausedUI.interactable = false;
        canvasGroupPausedUI.blocksRaycasts = false;
    }
 
}
