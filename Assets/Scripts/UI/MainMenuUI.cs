using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;


    private void Awake()
    {
        startButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.SampleScene);
        });

        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
