using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WinUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI winStatsTextMesh;
    [SerializeField] private CanvasGroup canvasGroupWinUI;
    [SerializeField] private Button menuButton;


    private void OnEnable()
    {
        Actions.OnGameWin += WinLevel;
    }


    private void OnDisable()
    {
        Actions.OnGameWin -= WinLevel;
    }


    private void Awake()
    {
        Hide();
        menuButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
        });
    }


    private void WinLevel(int score, float time)
    {
        Show();
        winStatsTextMesh.text =
            score.ToString() + "\n" +
            "\n" +
            TimeSpan.FromSeconds(time).ToString(@"mm\:ss");

    }


    private void Show()
    {
        canvasGroupWinUI.alpha = 0;

        float fadeTime = 1f;
        canvasGroupWinUI.DOFade(1, fadeTime).SetUpdate(true).OnComplete(() => {
            canvasGroupWinUI.interactable = true;
            canvasGroupWinUI.blocksRaycasts = true;
        });

        Time.timeScale = 0f;
    }


    private void Hide()
    {
        transform.DOKill();

        canvasGroupWinUI.alpha = 0;
        canvasGroupWinUI.interactable = false;
        canvasGroupWinUI.blocksRaycasts = false;

        Time.timeScale = 1f;
    }

}
