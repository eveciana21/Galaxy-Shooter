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

    void Start()
    {
        transform.position = new Vector3(Random.Range(9.4f, -9.4f), 7.5f, 0);

        _cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();

        _player = GameObject.Find("Player").GetComponent<Player>();


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

        StartCoroutine(EnemyFireDelayOnStart());
    }

    void Update()
    {
        CalculateMovement();
    }

    IEnumerator EnemyFireDelayOnStart()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1.0f, 2.5f));
            FireLaser();
        }
    }

    private void FireLaser()
    {
        if (Time.time > _canFireLaser)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFireLaser = Time.time + _fireRate;
            GameObject _enemyLaser = Instantiate(_enemyLaserPrefab, transform.position + new Vector3(0, -0.8f, 0), Quaternion.identity);
            Laser _lasers = _enemyLaser.GetComponent<Laser>();
            _lasers.EnemyFiredLaser();


            /*Laser[] _lasers = _enemyLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < _lasers.Length; i++)
            {
                _lasers[i].EnemyFiredLaser();
            } */

            /*for (int i = 0; i > _lasersChildren.Length; i++)
            {
                Instantiate(_laserAnim[i], transform.position, Quaternion.identity);
            }*/

        }


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
            //_speed = 1f;
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
