using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }


    [SerializeField] private int levelNumber;
    [SerializeField] private List<GameLevel> gameLevelList;
    [SerializeField] private int targetScore;

    private int score;
    private float currentSeconds;
    private bool hasWon;
    

    private void OnEnable()
    {
        Actions.OnPauseButtonPressed += Actions_OnPauseButtonPressed;
        Actions.OnAsteroidDestroyed += AddScore;
    }


    private void OnDisable()
    {
        Actions.OnPauseButtonPressed -= Actions_OnPauseButtonPressed;
        Actions.OnAsteroidDestroyed -= AddScore;
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

        hasWon = false;

        targetScore = 500;
    }


    private void Update()
    {
        currentSeconds += Time.deltaTime;

        if (score >= targetScore && !hasWon)
        {
            Actions.OnGameWin?.Invoke(score, currentSeconds);
            hasWon = true;
        }
    }


    private void Actions_OnPauseButtonPressed()
    {
        PauseUnpauseGame();
    }


    public void PauseUnpauseGame()
    {
        if (Time.timeScale == 1f)
        {
            PauseGame();
        }
        else
        {
            UnpauseGame();
        }
    }


    public void PauseGame()
    {
        Time.timeScale = 0f;
        Actions.OnGamePaused?.Invoke();
    }


    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        Actions.OnGameUnpaused?.Invoke();
    }


    public void AddScore(int addScoreAmount)
    {
        score += addScoreAmount;
    }

    
    public int GetScore()
    { 
        return score;
    }


    public float GetSeconds()
    {
        return currentSeconds;
    }

}
