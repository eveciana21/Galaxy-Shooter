using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySideMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private GameObject _greenExplosionPrefab;

    private float _fireRate;
    private float _canFireLaser;
    [SerializeField] private GameObject _greenFlagellum;
    private Player _player;
    private CameraShake _cameraShake;

    private float _random;
    private float _ping, _pong;
    private int _direction = -1;

    private bool _beginFiring;

    private int _lives = 3;

    [SerializeField] private Renderer _sprite;

    [SerializeField] AudioClip _alienSpawnAudio;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();

        StartCoroutine(FireLaserDelay());

        transform.position = new Vector3(Random.Range(8.5f, -8.5f), 8.3f, 0);

        _sprite = gameObject.GetComponentInChildren<Renderer>();

        _random = 0.25f;

        _ping = transform.position.x;
        _ping = _ping + _random;

        _pong = transform.position.x;
        _pong = _pong - _random;
    }

    void Update()
    {
        SideMovement();

        if (transform.position.y < -6.5)
        {
            Destroy(this.gameObject);
        }

        if (_beginFiring == true)
        {
            FireLaser();
        }
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
        yield return new WaitForSeconds(Random.Range(1.5f, 2.5f));
        FireLaser();
        _beginFiring = true;
    }

    void FireLaser()
    {
        if (_player != null)
        {
            if (Time.time > _canFireLaser)
            {
                _fireRate = Random.Range(3f, 5f);
                _canFireLaser = Time.time + _fireRate;

                if (transform.position.y > 0f)
                {
                    AudioSource.PlayClipAtPoint(_alienSpawnAudio, transform.position, 1);

                    Instantiate(_greenFlagellum, transform.position + new Vector3(0, -1.7f, 0), Quaternion.identity);
                }
            }
        }
    }

    private void Damage()
    {
        _lives--;
        _speed = _speed / 1.5f;
        StartCoroutine(DamageFlicker());

        if (_lives < 1)
        {
            _player.EnemyAlienGreenExplosion();

            _cameraShake.EnemyScreenShake();
            Instantiate(_greenExplosionPrefab, transform.position, Quaternion.identity);
            if (_player != null)
            {
                _player.AddToScore(100);
                _player.CurrentKillCount();
            }
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