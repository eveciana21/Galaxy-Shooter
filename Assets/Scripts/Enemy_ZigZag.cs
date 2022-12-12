using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ZigZag : MonoBehaviour
{
    private Player _player;

    private float _speed = 3;
    private float _random;
    private float _startYPos;

    private int _randomNumber;
    private int _direction = 1;

    [SerializeField] private GameObject _explosionPrefab;

    void Start()
    {
        _random = Random.Range(2f, 4.25f);

        _randomNumber = Random.Range(0, 2);

        if (_randomNumber == 0)
        {
            transform.position = new Vector3(-11.5f, _random, 0);
        }
        else if (_randomNumber == 1)
        {
            transform.position = new Vector3(11.5f, _random, 0);
        }

        _startYPos = transform.position.y;

        transform.position = new Vector3(transform.position.x, _startYPos + Mathf.Sin(Time.time * _random), transform.position.z);
    }

    void Update()
    {
        Debug.Log("Random Number " + _randomNumber);

        if (_randomNumber == 0)
        {
            _direction = -1;
        }
        else if (_randomNumber == 1)
        {
            _direction = 1;
        }

        transform.Translate(Vector3.left * _speed * _direction * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, _startYPos + Mathf.Sin(Time.time * _random), transform.position.z);

        if (transform.position.x < -13f || transform.position.x > 13f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)

    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _player.AddToScore(50);
            Destroy(other.gameObject);
            Destroy(this.gameObject, 0.1f);
        }
    }

}
