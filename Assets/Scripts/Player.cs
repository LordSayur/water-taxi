using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    private float _movementSpeed = 10f;
    [SerializeField]
    private bool _clampMovement = true;
    [SerializeField]
    private Vector2 _padding = new Vector2(1, 30);
    [SerializeField]
    private int _health = 200;

    [Header("Projectile")]
    [SerializeField]
    private float _firingRate = 1f;
    [SerializeField]
    private GameObject bulletPrefab;

    private Vector2 _movementDirection;
    private ScreenClamp screenClamp;
    private Coroutine fireContinously;

    private void Awake()
    {
        SetBoundary();
    }

    private void Update()
    {
        Move(_movementDirection.y, _movementDirection.x, _movementSpeed, _clampMovement);
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleHit(other);
    }

    public void GetAxis(InputAction.CallbackContext context)
    {
        _movementDirection = context.ReadValue<Vector2>();
    }
    
    public void Fire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            fireContinously = StartCoroutine(FireContinuously());
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            StopCoroutine(fireContinously);
        }
    }

    private void HandleHit(Collider other)
    {
        var damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer is null) return;
        _health -= damageDealer.GetDamage();
        Destroy(other.gameObject);

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Move(float forward, float sideway, float speed, bool isClamped = true)
    {
        float newForward, newSideway;

        float deltaForward = forward * speed * Time.deltaTime;
        float deltaSideway = sideway * speed * Time.deltaTime;

        Vector3 newPosition;

        if (isClamped)
        {
            newForward = Mathf.Clamp(
                transform.position.z + deltaForward, 
                screenClamp.LeftBottom.y, 
                screenClamp.RightTop.y);
            
            newSideway = Mathf.Clamp(
                transform.position.x + deltaSideway, 
                screenClamp.LeftBottom.x, 
                screenClamp.RightTop.x);

            //newPosition = mainCamera.transform.TransformPoint(
            //    new Vector3(newForward, newSideway, localPos.z));

            newPosition = new Vector3(newSideway, transform.position.y, newForward);
        }
        else
        {
            newForward = transform.position.z + forward * speed * Time.deltaTime;
            newSideway = transform.position.x + sideway * speed * Time.deltaTime;

            newPosition = new Vector3(newSideway, transform.position.y, newForward);
        };

        transform.position = newPosition;
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.layer = 10;
            yield return new WaitForSeconds(_firingRate);
        }
    }

    private void SetBoundary()
    {
        var mainCamera = Camera.main;

        Vector3 localPos = mainCamera.transform.InverseTransformPoint(transform.position);
        float dist = localPos.z;
        screenClamp.LeftBottom = 
            mainCamera.ViewportToWorldPoint(new Vector3(0, 0, dist));
        screenClamp.RightTop = 
            mainCamera.ViewportToWorldPoint(new Vector3(1, 1, dist));
        screenClamp.LeftBottom = 
            mainCamera.transform.InverseTransformPoint(screenClamp.LeftBottom);
        screenClamp.RightTop = 
            mainCamera.transform.InverseTransformPoint(screenClamp.RightTop);

        screenClamp.LeftBottom.x += _padding.x;
        screenClamp.LeftBottom.y += _padding.x;
        screenClamp.RightTop.x -= _padding.x;
        screenClamp.RightTop.y -= _padding.y;
    }

    private struct ScreenClamp
    {
        public Vector3 LeftBottom;
        public Vector3 RightTop;
    }
}
