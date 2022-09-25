using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _speed = 2.5f;
    private float _speedMultiplier = 2f;

    [SerializeField] private GameObject _laserPrefab;

    [SerializeField] private float _canFire = 0f;
    [SerializeField] private float _fireRate = 0.35f;

    [SerializeField] private int _playerLives = 3;

    private SpawnManager _spawnManager;
    [SerializeField] private bool _tripleShotIsActive;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private bool _isSpeedPowerUpActive;
    [SerializeField] private bool _isShieldPowerUpActive;
    [SerializeField] GameObject _shieldPrefab;



    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        
        if (_spawnManager == null)
        {
            Debug.Log("Spawn Manager is null");
        }    

    }

    void Update()
    {
        ControlMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

    }
    void ControlMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        //transform.Translate(Vector3.right * horizontal * _speed * Time.deltaTime);
        float vertical = Input.GetAxis("Vertical");
        //transform.Translate(Vector3.up * vertical * _speed * Time.deltaTime);
        
        Vector3 direction = new Vector3(horizontal, vertical, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        //X position
        if (transform.position.x > 11.25f)
        {
            transform.position = new Vector3(-11.25f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.25f)
        {
            transform.position = new Vector3(11.25f, transform.position.y, 0);
        }
        //Y position
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.25f, 5.25f), 0);
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (_tripleShotIsActive == true)
        {
            _canFire = Time.time + _fireRate;
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else 
        { 
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.2f, 0), Quaternion.identity);
        }
    }


    public void Damage()
    {
        if (_isShieldPowerUpActive == true)
        {
            _isShieldPowerUpActive = false;
            return;

        }
        _playerLives -= 1;
        
        if (_playerLives < 1)
        {
            _spawnManager.PlayerDead();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _tripleShotIsActive = true;
        StartCoroutine(TripleShotActiveTime());
    }

    IEnumerator TripleShotActiveTime()
    {
        if(_tripleShotIsActive==true)
        {
            yield return new WaitForSeconds(10);
            _tripleShotIsActive = false;
        }
    }


    public void SpeedBoostActive()
    {
        _isSpeedPowerUpActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostActiveTime());
    }


    IEnumerator SpeedBoostActiveTime()
    {
        if (_isSpeedPowerUpActive == true)
        {
            _speed /= _speedMultiplier;
            yield return new WaitForSeconds(5);
            _isSpeedPowerUpActive = false;
        }
    }

    public void ShieldActive ()
    {
        _isShieldPowerUpActive = true;
        Instantiate(_shieldPrefab, transform.position, Quaternion.identity);
        StartCoroutine(ShieldActiveTime());
    }

    IEnumerator ShieldActiveTime()
    {
        if (_isShieldPowerUpActive == true)
        {
            yield return new WaitForSeconds(10);
            _isShieldPowerUpActive = false;
        }
    }
    
}













