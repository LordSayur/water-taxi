using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int _health = 200;

    private void OnTriggerEnter(Collider other)
    {
        ProcessHit(other);
    }

    private void ProcessHit(Collider other)
    {
        var damageDealer = other.GetComponent<DamageDealer>();
        _health -= damageDealer.GetDamage();

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
