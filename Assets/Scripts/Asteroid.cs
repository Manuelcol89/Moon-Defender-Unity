using System.Collections;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid : MonoBehaviour
{

    [SerializeField] private int asteroidDamage;

    private Rigidbody2D rb;
    private HealthHandler asteroidHealthHandler;
    private SpriteRenderer asteroidSpriteRenderer;
    private Coroutine flashWhenHittedRoutine;

    private float rotationSpeed;

    private readonly int moonLayer = 3;
    private readonly int asteroidsLayer = 7;
    private readonly int playerLayer = 8;
    private readonly int bulletsLayer = 9;
    private readonly int wallUpLayer = 10;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        asteroidHealthHandler = GetComponent<HealthHandler>();
        asteroidSpriteRenderer = GetComponent<SpriteRenderer>();

        // Ignore asteroids collisions with top wall and player
        Physics2D.IgnoreLayerCollision(asteroidsLayer, playerLayer);
        Physics2D.IgnoreLayerCollision(asteroidsLayer, wallUpLayer);
    }


    private void Update()
    {
        DestroyAsteroid();
    }


    private void FixedUpdate()
    {
        AsteroidRotation();
    }


    private void AsteroidRotation()
    {
        float minRotation = -.5f;
        float maxRotation = .5f;
        // Add little random rotation to every asteroid
        rotationSpeed = Random.Range(minRotation, maxRotation);
        rb.AddTorque(rotationSpeed);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject != null)
        {
            var collidedGameObject = collision.gameObject;

            // If collided with moon surface, set its health to 0 and deal some damage to the moon
            if (collidedGameObject.layer == moonLayer && gameObject != null)
            {
                HealthHandler moonHealthHandler = collidedGameObject.GetComponent<HealthHandler>();
                asteroidHealthHandler.ChangeHealth(-asteroidHealthHandler.CurrentHealth);
                moonHealthHandler.ChangeHealth(asteroidDamage);

                SpriteShapeRenderer moonSpriteShapeRenderer = collidedGameObject.GetComponentInChildren<SpriteShapeRenderer>();
                if (flashWhenHittedRoutine != null) StopCoroutine(flashWhenHittedRoutine);
                flashWhenHittedRoutine = StartCoroutine(FlashRedSpriteShapeRenderer(moonSpriteShapeRenderer));
            }

            // If collided with bullet, take some damage and flash red color
            if (collidedGameObject.layer == bulletsLayer && gameObject != null)
            {
                Bullet bullet = collidedGameObject.GetComponent<Bullet>();
                asteroidHealthHandler.ChangeHealth(bullet.GetDamage);
                Actions.OnBulletDestroyed?.Invoke();
                Destroy(collidedGameObject);

                if (flashWhenHittedRoutine != null) StopCoroutine(flashWhenHittedRoutine);
                flashWhenHittedRoutine = StartCoroutine(FlashRedSpriteRenderer(asteroidSpriteRenderer));


                if (asteroidHealthHandler.CurrentHealth <= 0)
                    Actions.OnAsteroidDestroyed?.Invoke(-asteroidDamage);
            }
        }
    }

    // For flashing SpriteRenderer components
    private IEnumerator FlashRedSpriteRenderer(SpriteRenderer spriteRenderer)
    {
        float duration = 0.2f;
        Color originalColor = Color.white;
        Color flashColor = Color.red;

        // Flash to Red
        float elapsed = 0f;
        while (elapsed < duration / 2)
        {
            spriteRenderer.color = Color.Lerp(originalColor, flashColor, elapsed / (duration / 2));
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Back to Original
        elapsed = 0f;
        while (elapsed < duration / 2)
        {
            spriteRenderer.color = Color.Lerp(flashColor, originalColor, elapsed / (duration / 2));
            elapsed += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = originalColor;
    }

    // For flashing SpriteShapeRenderer components
    private IEnumerator FlashRedSpriteShapeRenderer(SpriteShapeRenderer spriteShapeRenderer)
    {
        float duration = 0.2f;
        Color originalColor = Color.white;
        Color flashColor = Color.red;

        // Flash to Red
        float elapsed = 0f;
        while (elapsed < duration / 2)
        {
            spriteShapeRenderer.color = Color.Lerp(originalColor, flashColor, elapsed / (duration / 2));
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Back to Original
        elapsed = 0f;
        while (elapsed < duration / 2)
        {
            spriteShapeRenderer.color = Color.Lerp(flashColor, originalColor, elapsed / (duration / 2));
            elapsed += Time.deltaTime;
            yield return null;
        }

        spriteShapeRenderer.color = originalColor;
    }


    private void DestroyAsteroid()
    {
        if (asteroidHealthHandler != null && asteroidHealthHandler.CurrentHealth <= 0)
        {
            float dissolveSpeed = 1f;
            
            DissolveEffect dissolveEffect = GetComponent<DissolveEffect>();
            dissolveEffect.StartDissolve(dissolveSpeed);
            this.GetComponent<CircleCollider2D>().enabled = false;
            Destroy(gameObject, 1.5f);
        }
    }
}