using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySideMovement : MonoBehaviour
{
    private float _speed = 1.8f;

    [SerializeField] private GameObject _explosionPrefab;
    float _random;

    private float _fireRate;
    private float _canFireLaser;
    [SerializeField] private GameObject _laserPrefab;
    Player _player;

    void Start()
    {
        transform.position = new Vector3(Random.Range(-8.5f, 8.5f), 8, 0);

        _random = Random.Range(1f, 7f);

        _player = GameObject.Find("Player").GetComponent<Player>();

    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        transform.position = new Vector3(Mathf.PingPong(Time.time, _random), transform.position.y, transform.position.z);

        if (transform.position.y < -6f)
        {
            transform.position = new Vector3(Random.Range(-8.5f, 8.5f), 8, 0);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -8.5f, 8.5f), transform.position.y, 0);

        FireLaser();
    }

    void FireLaser()
    {
        if (Time.time > _canFireLaser)
        {
            _fireRate = Random.Range(1.5f, 4f);
            _canFireLaser = Time.time + _fireRate;
            GameObject _enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] _laser = _enemyLaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i > _laser.Length; i++)
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
            _player.CurrentKillCount();
            Destroy(this.gameObject, 0.05f);

        }
    }

}





