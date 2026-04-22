using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletSpawn : MonoBehaviour
{

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerInputActions playerInputActions;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int bulletMaxCountShooting;
    [SerializeField] private float force;

    private Vector3 rifleEndPointPosition;
    private Vector3 lastRifleEndPointPosition;
    private Vector3 shootDirection;
    private Vector3 mouseWorldPosition;
    private RaycastHit2D raycastHit2D;
    private Transform aimRifleEndPointTransform;
    private float angle;
    private bool isShooting;
    private int maxBulletCount = 0;
    private int bulletCount;

    private const float MIN_ANGLE = -80f;
    private const float MAX_ANGLE = 80f;


    private enum Weapon
    {
        Rifle,
        MachineGun,
    }

    private Weapon weapon;

    

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
        playerInputActions.Player.Shoot.performed += (context) => Shoot(context);
        Actions.OnBulletDestroyed += BulletDestroyed;
    }


    private void OnDisable()
    {
        playerInputActions.Player.Disable();
        playerInputActions.Player.Shoot.performed -= (context) => Shoot(context);
        Actions.OnBulletDestroyed -= BulletDestroyed;
    }


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        aimRifleEndPointTransform = transform.Find("RifleEndPointPosition");
        rifleEndPointPosition = aimRifleEndPointTransform.position;
        isShooting = false;
        bulletCount = 0;
    }


    private void Start()
    {
        weapon = Weapon.Rifle;
    }


    private void Update()
    {
        HandleAiming();
    }


    private void FixedUpdate()
    {
        HandleRaycast();
    }


    private void HandleAiming()
    {
        // Get mouse position in world space
        mouseWorldPosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorldPosition.z = 0f;

        // Get the aim direction and angle
        var rightAngle = 90;
        Vector3 aimDirection = (mouseWorldPosition - rifleEndPointPosition).normalized;
        angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - rightAngle;

        if (angle >= MIN_ANGLE && angle <= MAX_ANGLE)
        {
            transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
        }

        // Update the starting position for shooting
        rifleEndPointPosition = aimRifleEndPointTransform.position;
    }


    private void HandleRaycast()
    {
        if (angle >= MIN_ANGLE && angle <= MAX_ANGLE)
        {
            var rayDistance = 50f;
            raycastHit2D = Physics2D.Raycast(rifleEndPointPosition, transform.up, rayDistance, layerMask);
            //Vector2 reflectPos = Vector2.Reflect(new Vector3(raycastHit2D.point.x, raycastHit2D.point.y) - rifleEndPointPosition, raycastHit2D.normal);

            //var reflectionDistance = 6f;
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, rifleEndPointPosition);
            lineRenderer.SetPosition(1, raycastHit2D.point);
            lineRenderer.SetPosition(2, raycastHit2D.point);

            aimRifleEndPointTransform.eulerAngles = new Vector3(0f, 0f, angle);
        }

        var littleDistanceOnYAxis = -2f;
        if (mouseWorldPosition.y < transform.position.y - littleDistanceOnYAxis)
        {
            lineRenderer.enabled = false;
        }
        else
        {
            lineRenderer.enabled = true;
        }
    }


    private void Shoot(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {

        switch (weapon)
        {
            case Weapon.Rifle:
                maxBulletCount = 8;
                if (obj.performed && isShooting == false && bulletCount < maxBulletCount)
                    {
                        lastRifleEndPointPosition = rifleEndPointPosition;
                        shootDirection = transform.up;
                        GameObject ball = Instantiate(GameAssets.i.bulletPrefab, lastRifleEndPointPosition, Quaternion.identity);
                        ball.GetComponent<Rigidbody2D>().AddForce(shootDirection * force, ForceMode2D.Force);
                        bulletCount++;
                    }
                break;

            case Weapon.MachineGun:
                maxBulletCount = 10;
                if (obj.performed && isShooting == false && bulletCount < maxBulletCount)
                {
                    lastRifleEndPointPosition = rifleEndPointPosition;
                    shootDirection = transform.up;
                    StartCoroutine(ShootBullets());
                }
                break;
        }
           
    }

    private IEnumerator ShootBullets()
    {
        isShooting = true;

        for (int i = 0; i < bulletMaxCountShooting; i++)
        {
            yield return new WaitForSeconds(0.2f);
            GameObject ball = Instantiate(GameAssets.i.bulletPrefab, lastRifleEndPointPosition, Quaternion.identity);
            ball.GetComponent<Rigidbody2D>().AddForce(shootDirection * force, ForceMode2D.Force);
            bulletCount++;
        }

        isShooting = false;
    }


    private void BulletDestroyed()
    {
        bulletCount--;
    }

}