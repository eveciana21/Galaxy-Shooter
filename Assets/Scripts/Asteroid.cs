using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Player _player;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private bool _isAsteroidDestroyed;
    SpawnManager _spawnManager;



    void Start()
    {
        transform.position = new Vector3(0, 4.5f, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {    

        transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.1f);
        }

        if (other.tag == "Player")
        {

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _player.Damage();
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.1f);
        }
    }







}
