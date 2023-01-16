using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenAttack : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _greenExplosion;

    private Transform _target;
    private Rigidbody2D _rb;

    [SerializeField] private float _rotateSpeed;

    [SerializeField] private Renderer _sprite;

    private int _lives = 2;

    private void Start()
    {
        _target = GameObject.Find("Player").transform;
        _rb = GetComponent<Rigidbody2D>();

        _sprite = gameObject.GetComponentInChildren<Renderer>();


        if (_target == null)
        {
            Debug.LogError("Player Target is Null");
        }
    }
    private void FixedUpdate()
    {
        if (_target != null)
        {
            Vector3 _direction = _target.position - (Vector3)_rb.position;

            _direction.Normalize();

            Vector3 transformDown = transform.up * -1;

            float rotateAmount = Vector3.Cross(_direction, transformDown).z;

            _rb.angularVelocity = -rotateAmount * _rotateSpeed;

            _rb.velocity = transformDown * _speed;
        }
        else
        {
            Instantiate(_greenExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.1f);
        }

        if (transform.position.x < -11f || transform.position.x > 11f || transform.position.y < -6f || transform.position.y > 8f)
        {
            Destroy(this.gameObject);
        }
    }

    private void Damage()
    {
        _lives--;

        if (_lives == 1)
        {
            _speed = _speed / 2;
            StartCoroutine(DamageFlicker());
        }
        if (_lives < 1)
        {
            Instantiate(_greenExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.05f);
        }
    }

    IEnumerator DamageFlicker()
    {
        _sprite.material.color = new Color(6, 1, 2);
        yield return new WaitForSeconds(0.1f);
        _sprite.material.color = Color.white;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Damage();
            Destroy(other.gameObject);
        }
    }
}
