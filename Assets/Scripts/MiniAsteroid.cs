using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniAsteroid : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedDown;
    private Player _player;
    [SerializeField] private GameObject _explosionPrefab;
    private float _rotation;

    [SerializeField] private GameObject _collectible;

    private CameraShake _cameraShake;

    private int _random;


    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();

        _rotation = Random.Range(-1f, 1f);

        _random = Random.Range(0, 3);

        if (_player == null)
        {
            Debug.Log("Player on Mini Asteroid is NULL");
        }

    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, _rotation) * _speed * Time.deltaTime);

        transform.Translate(Vector3.down * _speedDown * Time.deltaTime);

        if (transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            _player.AddToScore(10);
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            if (_collectible != null)
            {
                if (_random == 0)
                {
                    Instantiate(_collectible, transform.position, Quaternion.identity);
                }
            }

            Destroy(this.gameObject, 0.05f);
        }

        if (other.tag == "Player")
        {
            if (_player != null)
            {
                _player.Damage();
                _player.SubtractFromScore(10);
            }
            if (_cameraShake != null)
            {
                _cameraShake.CameraShaking();
            }
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }
    }

}
