using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{


    [SerializeField] private Button restartButton;


    private void Start()
    {
        restartButton.onClick.AddListener(() => { SceneLoader.LoadScene(SceneLoader.Scene.SampleScene); });
    }
}