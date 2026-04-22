using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;


    public int GetDamage => _damage;

    private int walls = 6;
    private int bulletsLayer = 9;
    private int wallUp = 10;


    private void Awake()
    {
        // Ignore physics collisions with other bullets
        Physics2D.IgnoreLayerCollision(bulletsLayer, bulletsLayer);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject != null)
        {
            var collidedGameObject = collision.gameObject;

            // If collided with walls surface, destroy itself
            if ((collidedGameObject.layer == walls || collidedGameObject.layer == wallUp) && gameObject != null)
            {
                Actions.OnBulletDestroyed?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
