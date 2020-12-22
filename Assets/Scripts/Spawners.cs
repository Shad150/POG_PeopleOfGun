using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    public PlayerMovement _playerMovement;
    public Weapon _gun;

    public Enemy _enemy;
    public EnemyWeapon _enemyGun;

    public Transform _spawner1;
    public Transform _spawner2;
    public Transform _spawner3;
    public Transform _spawner4;
    public Transform _spawner5;
    public Transform _spawner6;
    public Transform _spawner7;

    public Transform _respawnPos;

    bool _called;
    bool _enemyCalled;

    int _randomNumber;

    private void Start()
    {
        _randomNumber = Random.Range(0, 7);
        _respawnPos.position = _spawner1.position;
        _playerMovement.gameObject.transform.position = _respawnPos.position;
        
    }
    void Update()
    {
        if (_randomNumber == 0)
        {
            _respawnPos.position = _spawner1.position;
        }
        else if (_randomNumber == 1)
        {
            _respawnPos.position = _spawner2.position;
        }
        else if (_randomNumber == 2)
        {
            _respawnPos.position = _spawner3.position;
        }
        else if (_randomNumber == 3)
        {
            _respawnPos.position = _spawner4.position;
        }
        else if (_randomNumber == 4)
        {
            _respawnPos.position = _spawner5.position;
        }
        else if (_randomNumber == 5)
        {
            _respawnPos.position = _spawner6.position;
        }
        else if (_randomNumber == 6)
        {
            _respawnPos.position = _spawner7.position;
        }

        if (_playerMovement._dead)
        {
            if (!_called)
            {
                StartCoroutine(Dead());
                _called = true;
                _gun.gameObject.SetActive(false);
            }
        }

        if (_enemy._dead)
        {
            if (!_enemyCalled)
            {
                StartCoroutine(EnemyDead());
                _enemyCalled = true;
                _enemyGun.gameObject.SetActive(false);
            }
        }

    }

    IEnumerator Dead()
    {
        _randomNumber = Random.Range(0, 7);
        yield return new WaitForSeconds(5f);
        _playerMovement.gameObject.transform.position = _respawnPos.position;
        _playerMovement._dead = false;
        _gun.gameObject.SetActive(true);
        _gun._shooting = false;
        _called = false;
        _gun._bulletsInChamber = 6;
        _playerMovement._currentHealth = _playerMovement._maxHealth;


        Debug.Log("https://www.twitch.tv/shad150");
    }

    IEnumerator EnemyDead()
    {
        _randomNumber = Random.Range(0, 7);
        yield return new WaitForSeconds(5f);
        _enemy.gameObject.transform.position = _respawnPos.position;
        _enemy._dead = false;
        _enemyGun.gameObject.SetActive(true);
        _enemyGun._shooting = false;
        _enemyCalled = false;
        _enemyGun._bulletsInChamber = 6;

        _enemy._health = 5;

    }

}
