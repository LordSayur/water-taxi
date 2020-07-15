using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10f;

    private Vector2 movementDirection;

    private void Update()
    {
        Move(movementDirection.y, movementDirection.x, movementSpeed);
    }

    public void GetAxis(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();
    }

    private void Move(float forward, float sideway, float speed)
    {
        transform.position = new Vector3(
                    transform.position.x + sideway * speed * Time.deltaTime,
                    transform.position.y,
                    transform.position.z + forward * speed * Time.deltaTime
                    );
    }

}
