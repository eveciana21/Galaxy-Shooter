using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RammingEnemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _multipliedSpeed;

    private Rigidbody2D _rb;
    private Transform _target;

    [SerializeField] private float _rotateSpeed;

    private float minDistance = 4f;

    [SerializeField] GameObject _thruster;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _target = GameObject.Find("Player").transform;

        transform.position = new Vector3(Random.Range(-9, 9), 7.5f, 0);
    }

    void FixedUpdate()
    {
        Vector3 transformDown = -transform.up;
        float distance = Vector3.Distance(_target.position, _rb.position);

        if (distance < minDistance)
        {
            _thruster.SetActive(true);

            _multipliedSpeed = _speed * 1.5f;
            transform.Translate(Vector3.down * _multipliedSpeed * Time.deltaTime);

            Vector3 direction = _target.position - (Vector3)_rb.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transformDown).z;

            _rb.angularVelocity = -rotateAmount * _rotateSpeed;
        }
        else if (distance > minDistance)
        {
            _thruster.SetActive(false);
            
            float _rotateSpeedToZero = 10f;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.rotation.x,transform.rotation.y,0),Time.deltaTime *_rotateSpeedToZero);

            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }

        if(transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }
    }

}
