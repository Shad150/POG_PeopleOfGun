using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public SFX _sfx;

    [SerializeField]
    Transform _player;

    [SerializeField]
    Transform _holdingGun;

    [SerializeField]
    GameObject _enemyBullet;
    [SerializeField]
    Transform _enemyBulletSpawner;

    [SerializeField]
    ParticleSystem _shotEffect;
    [SerializeField]
    Transform _shotEffectSpawn;

    [SerializeField]
    ParticleSystem _casings;
    [SerializeField]
    Transform _casingsSpawn;

    public float _bulletsInChamber;
    public bool _reloading;

    public bool _shooting;
    public bool _playerAtRange;

    

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _holdingGun.transform.position;

        Vector2 _direction = new Vector2(transform.position.x - _player.position.x, transform.position.y - _player.position.y);
        float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, _angle + 180);

        if (_playerAtRange)
        {
            if (!_shooting && !_reloading)
            {
                if (_bulletsInChamber >= 1)
                {
                    _shooting = true;
                    StartCoroutine(Shooting());
                }
                else
                {
                    _reloading = true;
                    StartCoroutine(Reload());
                }
            }
        }
        

    }

    IEnumerator Shooting()
    {
        Instantiate(_enemyBullet, new Vector2(_enemyBulletSpawner.transform.position.x, _enemyBulletSpawner.transform.position.y), transform.rotation);

        Instantiate(_shotEffect, _shotEffectSpawn.transform.position, _shotEffectSpawn.transform.rotation, _shotEffectSpawn.transform);

        _sfx.ShotSound();

        yield return new WaitForSeconds(0.3f);
        _bulletsInChamber--;

        _shooting = false;
    }

    IEnumerator Reload()
    {
        _reloading = true;
        _sfx.ReloadSound();
        Instantiate(_casings, _casingsSpawn.transform.position, _casingsSpawn.transform.rotation, _casingsSpawn);
        yield return new WaitForSeconds(1.5f);
        _bulletsInChamber = 6;
        _reloading = false;
    }

}
