using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _bulletSpeed = 20f;
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rigidbody.velocity = new Vector3(0, 0, _bulletSpeed);
    }

    public void SetBulletSpeed(float speed)
    {
        _bulletSpeed = speed;
    }
}
