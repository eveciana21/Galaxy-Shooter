using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniAsteroid : MonoBehaviour
{


    [SerializeField] private float _speed;
    [SerializeField] private float _speedDown;
    private Player _player;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private bool _isAsteroidDestroyed;
    SpawnManager _spawnManager;



    void Update()
    {
        transform.Rotate(new Vector3(0, 0, Random.Range(-1, 1)) * _speed * Time.deltaTime);

        transform.Translate(Vector3.down * _speedDown * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {

            Destroy(other.gameObject);

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            Destroy(this.gameObject, 0.1f);
        }

        if (other.tag == "Player")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            _player.Damage();
            Destroy(this.gameObject, 0.1f);
        }
    }







}
