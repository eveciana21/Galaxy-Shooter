using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _speed;
    private float _speedMultiplier = 1.45f;

    [SerializeField] private GameObject _laserPrefab;

    [SerializeField] private float _canFire = 0f;
    [SerializeField] private float _fireRate = 0.25f;

    [SerializeField] private int _playerLives;

    private SpawnManager _spawnManager;
    [SerializeField] private bool _tripleShotIsActive;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private bool _isSpeedPowerUpActive;
    [SerializeField] private bool _isShieldPowerUpActive;
    [SerializeField] GameObject _shieldVisualizer;
    [SerializeField] GameObject _thrusterSpeed;
    [SerializeField] GameObject _thrusterMain;

    [SerializeField] GameObject _leftSmoke, _rightSmoke;
    [SerializeField] GameObject _explosionPrefab;

    private int _score;
    private UIManager _uiManager;
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(0.5f);
    private GameManager _gameManager;
    Animator _turnAnim;
    private bool _moveLeft, _moveRight;

    [SerializeField] AudioClip _laserAudio;
    [SerializeField] AudioClip _speedBoostPowerup;
    AudioSource _audioSource;



    void Start()
    {
        _shieldVisualizer.SetActive(false);
        _thrusterSpeed.SetActive(false);
        _leftSmoke.SetActive(false);
        _rightSmoke.SetActive(false);

        transform.position = new Vector3(0, -2.5f, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        _audioSource = GetComponent<AudioSource>();

        _turnAnim = gameObject.GetComponent<Animator>();



        if (_spawnManager == null)
        {
            Debug.Log("Spawn Manager is null");
        }
        if (_uiManager == null)
        {
            Debug.Log("UI Manager is null");
        }
        if (_gameManager == null)
        {
            Debug.LogError("Game Manager is null");
        }
        
        if (_audioSource == null)
        {
            Debug.LogError("AudioSource on player is null");
        }
        else
        {
            _audioSource.clip = _laserAudio;
        }

    }

    void Update()
    {

        ControlMovement();


        if (_moveLeft == true)

        {
            transform.Translate(new Vector3(-1, 0, 0) * _speed * Time.deltaTime);
            MovingAnimation();

        }


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
        if (_isSpeedPowerUpActive == true)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -2.50f, 5.25f), 0);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.00f, 5.25f), 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (_tripleShotIsActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.2f, 0), Quaternion.identity);
        }

        _audioSource.Play();
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

        if (_playerLives == 2)
        {
            _leftSmoke.SetActive(true);
        }
        else if (_playerLives == 1)
        {
            _rightSmoke.SetActive(true);
        }

        _uiManager.UpdateLives(_playerLives);


        if (_playerLives < 1)
        {
            _spawnManager.PlayerDead();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

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
        if (_tripleShotIsActive == true)
        {
            yield return new WaitForSeconds(10);
            _tripleShotIsActive = false;
        }
    }
    public void SpeedBoostActive()
    {
        _isSpeedPowerUpActive = true;

        StopCoroutine("SpeedBoostActiveTime");
        StartCoroutine("SpeedBoostActiveTime");
    }

    IEnumerator SpeedBoostActiveTime()
    {
        if (_isSpeedPowerUpActive == true)
        {
            AudioSource.PlayClipAtPoint(_speedBoostPowerup,transform.position);
            _thrusterSpeed.SetActive(true);
            _thrusterMain.SetActive(false);

            yield return new WaitForSeconds(10);

            _isSpeedPowerUpActive = false;
            _thrusterMain.SetActive(true);
            _thrusterSpeed.SetActive(false);
        }
    }

    public void ShieldActive()
    {
        _isShieldPowerUpActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void AddToScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }



    void MovingAnimation()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _moveLeft = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _moveRight = true;
        }
    }
}













