using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;

    [SerializeField]
    float _speed;

    [SerializeField]
    GameObject _raycast;

    void Start()
    {
        _raycast.transform.SetParent(null);
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = transform.right * _speed;
        
    }

    void Update()
    {
        _raycast.transform.position = gameObject.transform.position;

        Destroy(gameObject, 6f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
