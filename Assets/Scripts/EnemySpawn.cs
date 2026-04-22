using UnityEngine;
using System.Threading.Tasks;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Transform wallUp;
    [SerializeField] private Transform wallLeft;
    [SerializeField] private Transform wallRight;
    [SerializeField] private GameObject[] asteroids;
    [SerializeField] private float timer;

    private Vector3 startingPosition;
    private bool stopSpawning = false;


    private enum State
    {
        Idle,
        Active,
        Over,
    }

    private State state;

    private void Start()
    {
        {
            state = State.Active;
        }
    }

    private void Update()
    {
        switch (state)
        {
            case State.Active:
                if (timer >= 0)
                {
                    timer -= Time.deltaTime;
                    if (timer <= 0)
                    {
                        SpawnAsteroids();
                    }
                }
                else { timer = 1f; }
                break;

            case State.Over:
                StopSpawning();
                break;
        }
    }


    private void SpawnAsteroids()
    {
        float spawnXPositionRange = Random.Range(wallLeft.position.x, wallRight.position.x);
        float spawnYPosition = wallUp.position.y + 1.5f;
        float spawnZPosition = 0f;
        startingPosition = new Vector3(spawnXPositionRange, spawnYPosition, spawnZPosition);
        int randomAsteroidsIndex = Random.Range(0, asteroids.Length);
        Instantiate(asteroids[randomAsteroidsIndex], startingPosition, Quaternion.identity);
    }


    private void StopSpawning()
    {
        stopSpawning = true;
    }


    [System.Serializable]
    public class Wave
    {

    }

}



