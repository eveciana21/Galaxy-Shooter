using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineBall : MonoBehaviour
{
    private float _speed = 7f;
    private GameObject _target;

    private bool _continueMoving;

    private Vector3 _playerStartPos;
    private Vector3 _direction;

    void Start()
    {
        _target = GameObject.Find("Player");//.transform;

        if (_target != null)
        {
            _playerStartPos = _target.transform.position;
            _direction = (_playerStartPos - transform.position).normalized;
        }

    }

    void Update()
    {
        if (_target != null)
        {
            if (_continueMoving == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, _playerStartPos, _speed * Time.deltaTime);

                if (transform.position == _playerStartPos)
                {
                    _continueMoving = true;
                }
            }
            else
            {
                transform.Translate(_direction * _speed * Time.deltaTime);
            }
        }
        else
        {
            transform.Translate(-transform.up * _speed * Time.deltaTime);
        }


        if (transform.position.x > 11 || transform.position.x < -11 || transform.position.y < -6 || transform.position.y > 8)
        {
            Destroy(this.gameObject);
        }
    }
}
