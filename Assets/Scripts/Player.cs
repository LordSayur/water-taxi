using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 10f;
    [SerializeField]
    private bool _clampMovement = true;
    [SerializeField]
    private Vector2 padding = new Vector2(1, 30);

    private Vector2 _movementDirection;
    private ScreenClamp screenClamp;

    private void Awake()
    {
        SetBoundary();
    }

    private void Update()
    {
        Move(_movementDirection.y, _movementDirection.x, _movementSpeed, _clampMovement);
    }

    public void GetAxis(InputAction.CallbackContext context)
    {
        _movementDirection = context.ReadValue<Vector2>();
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

        screenClamp.LeftBottom.x += padding.x;
        screenClamp.LeftBottom.y += padding.x;
        screenClamp.RightTop.x -= padding.x;
        screenClamp.RightTop.y -= padding.y;
    }

    private struct ScreenClamp
    {
        public Vector3 LeftBottom;
        public Vector3 RightTop;
    }
}
