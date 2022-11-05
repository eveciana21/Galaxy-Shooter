using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterBrigade : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _fighterLaserPrefab;
    [SerializeField] private GameObject[] _fighterBrigadeArray;
    [SerializeField] private GameObject _explosionPrefab;

    private float _canFire = 0;
    private float _fireRate = 1.75f;

    void Start()
    {
        transform.position = new Vector3(0, -9, 0);
    }

    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > 8)
        {
            Destroy(this.gameObject);
        }


        if (Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;

            foreach (var fighter in _fighterBrigadeArray)
            {
                if (fighter != null)
                {
                    Instantiate(_fighterLaserPrefab, fighter.transform.position, Quaternion.identity);
                }
            }
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            Destroy(other.gameObject);

            Destroy(this.gameObject);
        }

        if (other.tag == "Laser")
        {
            if (transform.position.y > 8)
            {
                Destroy(other.gameObject);
            }
        }

        if (other.tag == "Enemy Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            Destroy(other.gameObject);

            Destroy(this.gameObject);
        }


    }
}


