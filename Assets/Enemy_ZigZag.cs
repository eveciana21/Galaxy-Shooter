using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ZigZag : MonoBehaviour
{
     private float _speed = 3;
    private float _random;
    private int _direction = 1;
    void Start()
    {
        //create a spawn from either side of the screen.

        _random = Random.Range(3, 8);
        transform.position = new Vector3(Random.Range(-9, 9), 4, 0);


    }

    void Update()
    {
        transform.Translate(new Vector3(transform.position.x, Mathf.PingPong(Time.time, 1f), 0) * _speed * Time.deltaTime);


        if (transform.position.x < -_random )
        {
            _direction = 1;
        }
        else if (transform.position.x >_random)
        {
            _direction = -1;
        }
        transform.Translate(Vector3.left * _speed * _direction * Time.deltaTime);

    }
}
