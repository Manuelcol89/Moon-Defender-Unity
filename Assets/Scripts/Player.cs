using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField] private PlayerInputActions playerInputActions;

    [SerializeField] private float moveSpeed = 40f;

    private Rigidbody2D playerRb2D;

    private float maxSpeed = 10f;
    private int playerLayer = 8;
    private int bulletLayer = 9;


    private void Awake()
    {
        playerRb2D = GetComponent<Rigidbody2D>();
        playerInputActions = new PlayerInputActions();
        Physics2D.IgnoreLayerCollision(playerLayer, bulletLayer);
    }



    private void FixedUpdate()
    {
        MovePlayer();
    }


    private void MovePlayer()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        playerRb2D.AddForceX(inputVector.x * moveSpeed, ForceMode2D.Force);

        if (playerRb2D.linearVelocity.magnitude > maxSpeed)
        {
            playerRb2D.linearVelocity = Vector3.ClampMagnitude(playerRb2D.linearVelocity, maxSpeed);
        }
    }


    private void OnEnable()
    {
        playerInputActions.Player.Enable();
    }


    private void OnDisable()
    {
        playerInputActions.Player.Disable();
    }
}
