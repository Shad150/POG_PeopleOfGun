using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerMovement : MonoBehaviour
{
    public Weapon _weapon;
    public HealthBar _healthBar;

    [SerializeField]
    float _speed;

    [SerializeField]
    float _jumpForce;

    bool _grounded;
    [SerializeField]
    LayerMask _groundLayer;

    [SerializeField]
    Rigidbody2D _rigidbody2D;

    [SerializeField]
    GameObject _gunAmin;
    [SerializeField]
    Transform _flipTrigger;

    [SerializeField]
    GameObject _bullet;

    [SerializeField]
    Animator _animator;

    public int _maxHealth;
    public int _currentHealth;

    public bool _dead;

    [SerializeField]
    GameObject _enemy;

    [SerializeField]
    bool _climb;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _currentHealth = _maxHealth;
        _healthBar.SetMaxHealth(_currentHealth);
    }

    void Update()
    {
        //Player movement

        if (!_dead)
        {
            if (!Physics2D.Raycast(transform.position, Vector2.right, 0.6f, _groundLayer ))
            {
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Translate(new Vector2(_speed, 0) * Time.deltaTime);
                }
            }

            if (!Physics2D.Raycast(transform.position, Vector2.left, 0.6f, _groundLayer))
            {
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Translate(new Vector2(-_speed, 0) * Time.deltaTime);

                }
            }

            if (_climb)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    transform.Translate(new Vector2(0, _speed) * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.S))
                {
                    transform.Translate(new Vector2(0, -_speed) * Time.deltaTime);
                }
            }


            _animator.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal")));

            //Player Jump

            Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.3f);

            if (Physics2D.Linecast(transform.position, transform.position + Vector3.down * 1.3f, _groundLayer))
            {
                _grounded = true;
            }
            else
            {
                _grounded = false;

            }

            if (_grounded && !_climb)
            {
                
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                }
            }

            _animator.SetBool("isGrounded", _grounded);

            //Camera Flip

            if (_gunAmin.transform.eulerAngles.z < 90 || (_gunAmin.transform.eulerAngles.z < 360 && _gunAmin.transform.eulerAngles.z > 270))
            {
                //Mirar derecha

                Vector3 scale = transform.localScale;
                scale.x = 1;
                transform.localScale = scale;

                _weapon.GetComponent<SpriteRenderer>().flipY = false;

            }
            else
            {
                //Mirar izquierda

                Vector3 scale = transform.localScale;
                scale.x = -1;
                transform.localScale = scale;

                _weapon.GetComponent<SpriteRenderer>().flipY = true;

            }
        }
        


        //Vida

        if (_currentHealth <= 0)
        {
            _dead = true;

        }

        _animator.SetBool("isDead", _dead);



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet2")
        {
            _currentHealth--;

            _healthBar.SetHealth(_currentHealth);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stairs")
        {
            _rigidbody2D.gravityScale = 0;
            _climb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stairs")
        {
            _rigidbody2D.gravityScale = 4;
            _climb = false;
        }
    }


}
