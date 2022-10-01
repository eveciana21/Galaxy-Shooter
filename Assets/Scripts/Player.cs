using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField] private float _speed = 2.5f;
    private float _speedMultiplier = 2f;

    [SerializeField] private GameObject _laserPrefab;

    [SerializeField] private float _canFire = 0f;
    [SerializeField] private float _fireRate = 0.35f;

    [SerializeField] private int _playerLives;

    private SpawnManager _spawnManager;
    [SerializeField] private bool _tripleShotIsActive;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private bool _isSpeedPowerUpActive;
    [SerializeField] private bool _isShieldPowerUpActive;
    [SerializeField] GameObject _shieldVisualizer;
    
    private int _score;
    private UIManager _uiManager;
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(0.5f);
    private GameManager _gameManager;
    



    void Start()
    {
        _shieldVisualizer.SetActive(false);

        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();


        if (_spawnManager == null)
        {
            Debug.Log("Spawn Manager is null");
        }

        if(_uiManager == null)
        {
            Debug.Log("UI Manager is null");
        }

        if(_gameManager == null)
        {
            Debug.LogError("Game Manager is null");
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


        if (_isSpeedPowerUpActive == false)
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * _speedMultiplier * Time.deltaTime);
            Debug.Log("Speed:" + _speed * _speedMultiplier);
        }
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
           // _canFire = Time.time + _fireRate;
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
            _shieldVisualizer.SetActive(false);
            return;
        }
        _playerLives -= 1;
        _uiManager.UpdateLives(_playerLives);


        
        if (_playerLives < 1)
        {
            _spawnManager.PlayerDead();
            Destroy(this.gameObject);
        }
    }


    public void TripleShotActive()
    {
        _tripleShotIsActive = true;
        StopCoroutine("TripleShotActiveTime");
        StartCoroutine("TripleShotActiveTime");
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
        StartCoroutine(SpeedBoostActiveTime());
    }
    
    IEnumerator SpeedBoostActiveTime()
    {
        if (_isSpeedPowerUpActive == true)
        {
            yield return new WaitForSeconds(10);
            _isSpeedPowerUpActive = false;
        }
    }

    public void ShieldActive ()
    {
        _isShieldPowerUpActive = true;
        _shieldVisualizer.SetActive(true);
    }

   public void AddToScore (int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);      
    }

}













