using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterBrigade : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _fighterLaserPrefab;
    [SerializeField] private GameObject[] _fighterBrigadeArray;
    [SerializeField] private GameObject _explosionPrefab;
    private CameraShake _camerShake;
    private bool _onScreen;

    private float _canFire = 0;
    private float _fireRate = 1.75f;

    void Start()
    {
        transform.position = new Vector3(0, -9, 0);
        _camerShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
    }

    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= -9f)
        {
            _onScreen = true;
        }
        if (transform.position.y >= 7f)
        {
            _onScreen = false;
            Destroy(this.gameObject);
        }

        FighterOnScreenCameraShake();

        //SPAWNS EACH FIGHTER IN THE BRIGADE
        if (Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;

            foreach (var fighter in _fighterBrigadeArray)
            {
                if (fighter != null)
                {
                    Instantiate(_fighterLaserPrefab, fighter.transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
                }
            }
        }
    }

    private void FighterOnScreenCameraShake()
    {
        if (_onScreen == true)
        {
            _camerShake.FighterBrigadeCameraShake();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject, 0.05f);
        }

        if (other.tag == "Laser")
        {
            if (other.transform.position.y > 8)
            {
                Destroy(other.gameObject);
            }
        }

        if (other.tag == "Enemy Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject, 0.05f);
        }
    }
}


