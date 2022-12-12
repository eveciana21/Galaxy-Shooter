using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySideMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1.8f;

    [SerializeField] private GameObject _explosionPrefab;

    private float _fireRate;
    private float _canFireLaser;
    [SerializeField] private GameObject _laserPrefab;
    Player _player;
    [SerializeField] GameObject[] _laserParent;

    private float _random;
    private float _ping, _pong;
    private int _direction = -1;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
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
            transform.position = new Vector3(transform.position.x, 8, 0);
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

    void FireLaser()
    {
        if (Time.time > _canFireLaser && transform.position.y < 6)
        {
            _fireRate = Random.Range(1.5f, 4f);
            _canFireLaser = Time.time + _fireRate;
            GameObject _enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] _laser = _enemyLaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i < _laser.Length; i++)
            {
                _laser[i].EnemyFiredLaser();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _player.AddToScore(50);
            _player.CurrentKillCount();

            Destroy(this.gameObject, 0.05f);
        }
    }


}





