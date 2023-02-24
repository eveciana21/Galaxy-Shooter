using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShielded : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _canFire = 1;
    private float _fireRate;

    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _shieldPrefab;

    [SerializeField] private int _lives = 2;

    private Player _player;

    [SerializeField] GameObject _tinyExplosion;
    [SerializeField] GameObject _explosion;

    private Transform _target;

    private float minDistance = 4f;

    [SerializeField] private float _rotateSpeed;

    private Rigidbody2D _rb;

    [SerializeField] private GameObject _thruster;

    [SerializeField] private GameObject _laserWarning;

    private int _randomNumber;

    [SerializeField] private AudioClip _laserAudio;

    [SerializeField] private GameObject _shieldBurstParticle;

    private CameraShake _cameraShake;


    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        _target = GameObject.Find("Player").transform;


        _rb = GetComponent<Rigidbody2D>();

        _cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();

        _laserWarning.gameObject.SetActive(false);
        _shieldBurstParticle.gameObject.SetActive(false);
        _thruster.gameObject.SetActive(false);

        transform.position = new Vector3(Random.Range(-9.4f, 9.4f), 7.5f, 0);
        _shieldPrefab.gameObject.SetActive(true);

        _randomNumber = Random.Range(0, 2);

    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (_randomNumber == 0)
        {
            FireLaser();
        }
    }

    private void FixedUpdate()
    {
        RammingAttack();
    }

    private void RammingAttack()
    {
        if (_target != null)
        {
            if (_lives == 1)
            {
                Vector3 transformDown = -transform.up;
                float distance = Vector3.Distance(_target.position, _rb.position);

                if (distance < minDistance)
                {
                    _thruster.SetActive(true);

                    Vector3 direction = _target.position - (Vector3)_rb.position;

                    direction.Normalize();

                    float rotateAmount = Vector3.Cross(direction, transformDown).z;

                    _rb.angularVelocity = -rotateAmount * _rotateSpeed;
                }
                else if (distance > minDistance)
                {
                    _thruster.SetActive(false);

                    float _rotateSpeedToZero = 15f;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0), Time.deltaTime * _rotateSpeedToZero);
                }
            }
        }
    }


    private void FireLaser()
    {
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(2f, 3.5f);
            _canFire = _fireRate + Time.time;

            StartCoroutine(LaserWarning());
        }
    }

    IEnumerator LaserWarning()
    {
        _laserWarning.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        if (_player != null)
        {
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position + new Vector3(0, -0.65f, 0), Quaternion.identity);
            Laser laser = enemyLaser.GetComponent<Laser>();
            laser.EnemyFiredLaser();
            AudioSource.PlayClipAtPoint(_laserAudio, transform.position, 1f);

            yield return new WaitForSeconds(0.1f);
            _laserWarning.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _lives--;
            _shieldBurstParticle.gameObject.SetActive(true);
            _shieldPrefab.gameObject.SetActive(false);

            if (_lives == 0)
            {
                Instantiate(_explosion, transform.position, Quaternion.identity);
                Destroy(this.gameObject, 0.1f);
            }
        }
        if (other.tag == "Laser")
        {
            _lives--;
            _shieldPrefab.gameObject.SetActive(false);

            if (_lives == 1)
            {
                _shieldBurstParticle.gameObject.SetActive(true);
            }
            else if (_lives == 0)
            {
                _player.AddToScore(50);
                Instantiate(_explosion, transform.position, Quaternion.identity);
                _cameraShake.EnemyScreenShake();
                Destroy(this.gameObject, 0.1f);
            }
            Destroy(other.gameObject);
        }
    }
}