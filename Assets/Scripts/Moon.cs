using UnityEngine;

[RequireComponent(typeof(HealthHandler))]
public class Moon : MonoBehaviour
{

    private HealthHandler healthHandler;


    private void Awake()
    {
        healthHandler = GetComponent<HealthHandler>();
    }

    private void Update()
    {
        if (healthHandler != null && healthHandler.CurrentHealth <=0)
        {
            Debug.Log("Game Over");
            SceneLoader.LoadScene(SceneLoader.Scene.GameOverScene);

        }
    }
}
