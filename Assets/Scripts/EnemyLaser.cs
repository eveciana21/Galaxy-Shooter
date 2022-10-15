using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] GameObject _tinyExplosionPrefab;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.5)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Instantiate(_tinyExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}
