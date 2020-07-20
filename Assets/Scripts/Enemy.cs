using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int _health = 200;
    [SerializeField]
    private float _shotCounter;
    [SerializeField]
    private float _minTimeBetweenShots = 0.2f;
    [SerializeField]
    private float _maxTimeBetweenShots = 3f;

    [SerializeField]
    private GameObject _bulletPrefab;

    private void Start()
    {
        _shotCounter = UnityEngine.Random.Range(_minTimeBetweenShots, _maxTimeBetweenShots);
        StartCoroutine(Fire());
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds(_shotCounter);
            var bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().SetBulletSpeed(-20);
            _shotCounter = UnityEngine.Random.Range(_minTimeBetweenShots, _maxTimeBetweenShots);
        }
    }

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
