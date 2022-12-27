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

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        transform.position = new Vector3(Random.Range(-9.4f, 9.4f), 7.5f, 0);
        _shieldPrefab.gameObject.SetActive(true);

        StartCoroutine(FireDelay());

    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

    }

    IEnumerator FireDelay ()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 2f));
            FireLaser();
        }
    }
    private void FireLaser()
    {
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(2f, 3.5f);
            _canFire = _fireRate + Time.time;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position + new Vector3(0, -0.65f, 0), Quaternion.identity);
            Laser laser = enemyLaser.GetComponent<Laser>();
            laser.EnemyFiredLaser();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _lives--;
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
                Instantiate(_tinyExplosion, transform.position + new Vector3(0, -0.8f, 0), Quaternion.identity);
            }
            else if (_lives == 0)
            {
                _player.AddToScore(50);
                Instantiate(_explosion, transform.position, Quaternion.identity);
                Destroy(this.gameObject, 0.1f);
            }
            Destroy(other.gameObject);
        }
    }


}
