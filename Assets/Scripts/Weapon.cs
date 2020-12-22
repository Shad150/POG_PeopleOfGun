using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Weapon : MonoBehaviour
{
    public SFX _sfx;

    [SerializeField]
    Image _counter;
    [SerializeField]
    List<Sprite> _counterStates = new List<Sprite>();

    [SerializeField]
    Transform _gunPosition;

    private Camera _cam;

    [SerializeField]
    GameObject _bullet;
    [SerializeField]
    Transform _spawner;

    public bool _shooting;
    [SerializeField]
    bool _reloading;
    
     public int _bulletsInChamber= 6;

    [SerializeField]
    ParticleSystem _casings;
    [SerializeField]
    Transform _casingsSpawn;

    [SerializeField]
    ParticleSystem _shotEffect;
    [SerializeField]
    Transform _shotEffectSpawn;

    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //Posición del arma, para seguir al Player

        transform.position = _gunPosition.position;


        //Rotar según la posición del ratón

        Vector3 _mousePosition = Input.mousePosition;

        Vector3 _screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);

        Vector2 _direction = new Vector2(_mousePosition.x - _screenPoint.x, _mousePosition.y - _screenPoint.y);

        float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, _angle);


        //Disparar

        if (!_shooting)
        {
            if (!_reloading)
            {
                if (_bulletsInChamber >= 1)
                {
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        _shooting = true;
                        StartCoroutine(Shooting());
                    }
                }
                else
                {
                        StartCoroutine(Reload());   
                }
            }
            

        }


        //Recarga

        if (_bulletsInChamber < 6)
        {
            if (!_reloading)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    StartCoroutine(Reload());
                }
            }
            
        }

        _counter.sprite = _counterStates[_bulletsInChamber];

    }

    IEnumerator Shooting()
    {
        Instantiate(_bullet, new Vector2(_spawner.transform.position.x, _spawner.transform.position.y), transform.rotation);
        
        Instantiate(_shotEffect, _shotEffectSpawn.transform.position, _shotEffectSpawn.transform.rotation, _shotEffectSpawn.transform);

        _sfx.ShotSound();

        //_counter.sprite = _counterStates[_bulletsInChamber -1];

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
        //_counter.sprite = _counterStates[0];
        _reloading = false;
    }

}
