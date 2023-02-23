using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ZigZag : MonoBehaviour
{
    private Player _player;

    [SerializeField] private float _speed;
    private float _random;
    private float _startYPos;

    private int _randomNumber;
    private int _direction = 1;
    private float _canFire;
    private float _fireRate;

    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private GameObject _energyBall;
    private bool _beginFiring;

    private CameraShake _cameraShake;


    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();

        _random = Random.Range(2f, 4.25f);

        _randomNumber = Random.Range(0, 2);

        StartCoroutine(FireLaserDelay());

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

        if (_beginFiring == true)
        {
            FireLaser();
        }

        if (transform.position.x < -12f || transform.position.x > 12f)
        {
            Destroy(this.gameObject);
        }

    }

    IEnumerator FireLaserDelay()
    {
        yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        FireLaser();
        _beginFiring = true;
    }

    void FireLaser()
    {
        if (Time.time > _canFire)
        {
            float _randomRange = Random.Range(2f, 4f);
            _fireRate = _randomRange;
            _canFire = Time.time + _fireRate;
            Instantiate(_energyBall, transform.position + new Vector3(0, -0.8f, 0), Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)

    {
        if (other.tag == "Laser")
        {
            _cameraShake.EnemyScreenShake();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            if (_player != null)
            {
                _player.AddToScore(50);
                _player.CurrentKillCount();
            }
            Destroy(other.gameObject);
            Destroy(this.gameObject, 0.075f);
        }
    }

}
