using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    
    [SerializeField] private GameObject[] _enemies;
    private Rigidbody2D _rb;
    
    private float _distanceToClosestEnemy;
    private GameObject _closestEnemy;

    private bool _closestEnemyFound;

    [SerializeField] private float _rotateSpeed = 750f;



    void Start()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        _rb = GetComponent<Rigidbody2D>();

        _distanceToClosestEnemy = Mathf.Infinity;
        _closestEnemy = null;
    }

    void Update()
    {
        HomingMissile();

        if (transform.position.y > 6.5f || transform.position.y < -5.2f || transform.position.x < -10.5f || transform.position.x > 10.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        MoveTowardsEnemy();
    }


    private void HomingMissile()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in _enemies)
        {
            if (enemy != null)
            {
                float distanceToEnemy = Vector3.Distance(_rb.transform.position, enemy.transform.position);

                if (distanceToEnemy < _distanceToClosestEnemy)
                {
                    _distanceToClosestEnemy = distanceToEnemy;
                    _closestEnemy = enemy;
                    _closestEnemyFound = true;
                }
            }
        }
    }

    private void MoveTowardsEnemy()
    {
        if (_closestEnemy != null)
        {
            if (_closestEnemyFound == true)
            {
                Vector3 direction = _closestEnemy.transform.position - (Vector3)_rb.position;
                direction.Normalize();

                float rotateAmount = Vector3.Cross(direction, transform.up).z;
                _rb.angularVelocity = -rotateAmount * _rotateSpeed;
                _rb.velocity = transform.up * _speed;
            }
        }
        else
        {
            _rb.velocity = transform.up * _speed;
            _rb.angularVelocity = 0f;
        }
    }
}