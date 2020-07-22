using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
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
    private float _explosionLifetime = 1f;

    [Header("SFX")]
    [SerializeField]
    [Range(0, 1)]
    private float _sfxVolume = 0.7f;
    [SerializeField]
    private AudioClip _explosionClip;
    [SerializeField]
    private AudioClip _bulletClip;

    private GameObject _bulletPrefab;
    private GameObject _explosionPrefab;

    private void Awake()
    {
        _bulletPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Bullet.prefab");
        _explosionPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Fire.prefab");
    }

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
            bullet.layer = 11;
            AudioSource.PlayClipAtPoint(_bulletClip, Camera.main.transform.position, _sfxVolume);
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
        
        if (damageDealer is null) return;
        _health -= damageDealer.GetDamage();

        Destroy(other.gameObject);
        if (_health <= 0)
        {
            var explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, _explosionLifetime);
            AudioSource.PlayClipAtPoint(_explosionClip, Camera.main.transform.position, _sfxVolume);
            Destroy(gameObject);
        }
    }
}
