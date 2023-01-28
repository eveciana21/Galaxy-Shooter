using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySideMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1.8f;

    [SerializeField] private GameObject _explosionPrefab;

    private float _fireRate;
    private float _canFireLaser;
    [SerializeField] private GameObject _greenFlagellum;
    private Player _player;
    private CameraShake _cameraShake;

    private float _random;
    private float _ping, _pong;
    private int _direction = -1;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
       
        StartCoroutine(FireLaserDelay());

        transform.position = new Vector3(Random.Range(8.5f, -8.5f), 8, 0);

        _random = 0.25f;//Random.Range(4f, 10f);

        _ping = transform.position.x;
        _ping = _ping + _random;

        _pong = transform.position.x;
        _pong = _pong - _random;
    }

    void Update()
    {
        SideMovement();

        if (transform.position.y < -8)
        {
            Destroy(this.gameObject);
        }

        FireLaser();
    }

    private void SideMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.x > _ping || transform.position.x >= 8.75f)
        {
            _direction = -1;
        }
        else if (transform.position.x < _pong || transform.position.x <= -8.75f)
        {
            _direction = 1;
        }

        transform.Translate(Vector3.right * _speed * _direction * Time.deltaTime);
    }

    IEnumerator FireLaserDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3.0f, 5.0f));
            FireLaser();
        }
    }
    void FireLaser()
    {
        if (_player != null)
        {
            if (Time.time > _canFireLaser && transform.position.y < 5f)
            {
                _fireRate = Random.Range(3f, 5f);
                _canFireLaser = Time.time + _fireRate;

                if (transform.position.y > -1.5f)
                {
                    Instantiate(_greenFlagellum, transform.position + new Vector3(0, -1.7f, 0), Quaternion.identity);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            _cameraShake.EnemyScreenShake();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            if (_player != null)
            {
                _player.AddToScore(50);
                _player.CurrentKillCount();
            }

            Destroy(this.gameObject, 0.05f);
        }
    }

    


}





