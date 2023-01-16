using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Player _player;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private GameObject _enemyLaserPrefab;
    private float _canFireLaser;
    private float _fireRate;
    [SerializeField] private bool _damageTaken;
    private CameraShake _cameraShake;

    private bool _laserFiredUp;
    private bool _fireAtPowerup;
    private bool _warningGiven;

    private float minDistance = 4f;

    private Rigidbody2D _rb;

    [SerializeField] private GameObject _laserWarning;

    private int _randomNumber;

    void Start()
    {
        transform.position = new Vector3(Random.Range(9.4f, -9.4f), 7.35f, 0);

        _cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();

        _player = GameObject.Find("Player").GetComponent<Player>();

        _rb = GetComponent<Rigidbody2D>();

        _laserWarning.gameObject.SetActive(false);

        _randomNumber = Random.Range(0, 2);

        if (_player == null)
        {
            Debug.LogError("Player is null");
        }

        if (_explosionPrefab == null)
        {
            Debug.LogError("explosion prefab is null");
        }

        if (_enemyLaserPrefab == null)
        {
            Debug.LogError("Enemy laser is null");
        }


    }

    void Update()
    {
        CalculateMovement();

        if (_randomNumber == 0)
        {
            FireLaser();
        }
    }

    private void FireLaser()
    {
        Debug.Log("Random Number = " + _randomNumber);

        if (Time.time > _canFireLaser)
        {
            _fireRate = Random.Range(2f, 4f);
            _canFireLaser = Time.time + _fireRate;

            StartCoroutine(LaserWarning());
        }
    }

    IEnumerator LaserWarning()
    {
        _laserWarning.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        if (_player != null)
        {
            float distanceX = Mathf.Abs(_player.transform.position.x - _rb.transform.position.x);

            if (_laserFiredUp == false && _player.transform.position.y > _rb.position.y && distanceX < minDistance && transform.position.y > -4f)
            {
                GameObject enemyLaser = Instantiate(_enemyLaserPrefab, transform.position + new Vector3(0, 0.75f, 0), Quaternion.identity);
                Laser laser = enemyLaser.GetComponent<Laser>();
                laser.FireLaserAtPlayer();
                _laserFiredUp = true;
                _laserWarning.gameObject.SetActive(false);
            }
            else
            {
                if (_randomNumber == 0 || _fireAtPowerup == true)
                {
                    GameObject enemyLaser = Instantiate(_enemyLaserPrefab, transform.position + new Vector3(0, -0.65f, 0), Quaternion.identity);
                    Laser laser = enemyLaser.GetComponent<Laser>();
                    laser.EnemyFiredLaser();
                    _fireAtPowerup = false;
                    _laserWarning.gameObject.SetActive(false);
                }
            }
        }
    }

    public void FireAtPowerup()
    {
        _fireAtPowerup = true;
        StartCoroutine(LaserWarning());
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        //if Enemy position reaches bottom of screen
        if (transform.position.y <= -6.5f)
        {
            //spawn at random x position
            float randomX = Random.Range(9, -9);
            transform.position = new Vector3(randomX, 8, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddToScore(50);
                _player.CurrentKillCount();
                _cameraShake.EnemyScreenShake();
            }

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.05f);
        }

        if (other.tag == "Fighters")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }








}
