using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyWeapon _enemyWeapon;

    [SerializeField]
    float _speed;

    [SerializeField]
    Transform _player;
    [SerializeField]
    float _enemySight;
    [SerializeField]
    float _shootingRange;

    [SerializeField]
    Rigidbody2D _rigidbody2D;
    [SerializeField]
    float _jumpForce;

    [SerializeField]
    LayerMask _groundLayer;
    [SerializeField]
    bool _grounded;

    [SerializeField]
    GameObject _gun;

    public int _health;
    public bool _dead;

    [SerializeField]
    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _health = 5;
    }

    // Update is called once per frame
    void Update()
    {
        float _distanceFromPlayer = Vector2.Distance(_player.position, transform.position);

        if (!_dead)
        {
            if (_distanceFromPlayer < _enemySight && _distanceFromPlayer > _shootingRange)
            {
                transform.position = Vector2.MoveTowards(gameObject.transform.position, _player.position, _speed * Time.deltaTime);

                if (Physics2D.Linecast(transform.position, transform.position + Vector3.right * 1f, _groundLayer))
                {
                    _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                }

                if (Physics2D.Linecast(transform.position, transform.position + Vector3.left * 1f, _groundLayer))
                {
                    _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                }
            }

            if (_distanceFromPlayer <= _shootingRange)
            {
                _enemyWeapon._playerAtRange = true;
            }
            else
            {
                _enemyWeapon._playerAtRange = false;
            }

            if (_gun.transform.eulerAngles.z < 90 || (_gun.transform.eulerAngles.z < 360 && _gun.transform.eulerAngles.z > 270))
            {
                //Mirar derecha

                Vector3 scale = transform.localScale;
                scale.x = 1;
                transform.localScale = scale;

                _enemyWeapon.GetComponent<SpriteRenderer>().flipY = false;

            }
            else
            {
                //Mirar izquierda

                Vector3 scale = transform.localScale;
                scale.x = -1;
                transform.localScale = scale;

                _enemyWeapon.GetComponent<SpriteRenderer>().flipY = true;

            }


            if (Physics2D.Linecast(transform.position, transform.position + Vector3.down * 1.3f, _groundLayer))
            {
                _grounded = true;
            }
            else
            {
                _grounded = false;

            }

            Debug.DrawLine(transform.position, transform.position + Vector3.right * 1f);
            _animator.SetBool("isGrounded", _grounded);


        }


        if (_health <= 0)
        {
            _dead = true;

        }

        _animator.SetBool("isDead", _dead);


        Vector2 _targetDirection = (_player.position - _gun.transform.position).normalized * _speed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _shootingRange);
        Gizmos.DrawWireSphere(transform.position, _enemySight);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet1")
        {
            _health--;
        }
    }


    


}
