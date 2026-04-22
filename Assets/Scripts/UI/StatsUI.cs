using UnityEngine;
using TMPro;
using System;
public class StatsUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreTextMesh;
    [SerializeField] private TextMeshProUGUI timeTextMesh;


    private void Update()
    {
        UpdateStatsTextMesh();
    }


    private void UpdateStatsTextMesh()
    {
        scoreTextMesh.text = "Score: " + GameManager.Instance.GetScore();
        timeTextMesh.text = TimeSpan.FromSeconds(GameManager.Instance.GetSeconds()).ToString(@"mm\:ss");
    }


}
