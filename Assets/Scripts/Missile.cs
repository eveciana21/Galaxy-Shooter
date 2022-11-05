using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Transform _enemyTransform;

    void Start()
    {
        _enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    void Update()
    {
        if (transform.position.y > 8)
        {
            Destroy(this.gameObject);
        }

        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        transform.LookAt(_enemyTransform);

    }
}
