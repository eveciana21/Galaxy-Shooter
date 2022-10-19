using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _speed;
    private float _speedMultiplier = 1.8f;//1.45f;

    [SerializeField] private GameObject _laserPrefab;

    [SerializeField] private float _canFire = 0f;
    [SerializeField] private float _fireRate = 0.25f;

    [SerializeField] private int _playerLives;

    private SpawnManager _spawnManager;
    [SerializeField] private bool _tripleShotIsActive;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private bool _isSpeedPowerUpActive;
    [SerializeField] private bool _isShieldPowerUpActive;
    [SerializeField] private int _shieldHealth;

    //[SerializeField] private GameObject _shieldVisualizer;

    [SerializeField] private GameObject _thrusterSpeed;
    [SerializeField] private GameObject _thrusterMain;
    [SerializeField] private GameObject _leftThruster, _rightThruster;


    [SerializeField] private GameObject _leftSmoke, _rightSmoke;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private GameObject _tinyExplosionPrefab;


    private int _score;
    private UIManager _uiManager;
    private WaitForSeconds _waitForSeconds02 = new WaitForSeconds(0.15f);
    private GameManager _gameManager;
    private Animator _turnAnim;

    [SerializeField] private AudioClip _laserAudio;
    private AudioSource _audioSource;

    private Renderer _shieldColor;
    private SpriteRenderer _spriteRenderer;

    private Color _originalColor = new Color(1, 1, 1, 1);
    private Color _colorOne = new Color(12, 0, 91);
    private Color _colorTwo = new Color(0, 3, 10);

    private bool _damageTaken;

    private Collider2D _collider;






    void Start()
    {
        //_shieldVisualizer.SetActive(false);
        _thrusterSpeed.SetActive(false);
        _leftSmoke.SetActive(false);
        _rightSmoke.SetActive(false);

        transform.position = new Vector3(0, -2.5f, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        _audioSource = GetComponent<AudioSource>();

        _turnAnim = gameObject.GetComponent<Animator>();

        _shieldColor = GetComponent<Renderer>();

        _collider = GetComponent<Collider2D>();

        _spriteRenderer = GetComponent<SpriteRenderer>();



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


        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire && _damageTaken == false)
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

        //TURNING ANIMATION
        if (horizontal < 0)
        {
            _turnAnim.SetBool("Left_Turn_anim", true);
            _turnAnim.SetBool("Right_Turn_anim", false);
        }
        else if (horizontal > 0)
        {
            _turnAnim.SetBool("Right_Turn_anim", true);
            _turnAnim.SetBool("Left_Turn_anim", false);
        }
        else
        {
            _turnAnim.SetBool("Right_Turn_anim", false);
            _turnAnim.SetBool("Left_Turn_anim", false);
        }

        //SPEED POWERUP
        if (Input.GetKey(KeyCode.LeftShift) && _isSpeedPowerUpActive == true && _damageTaken == false)
        {
            transform.Translate(direction * _speed * _speedMultiplier * Time.deltaTime);
            _thrusterSpeed.SetActive(true);
            _thrusterMain.SetActive(false);
            Debug.Log("Speed:" + _speed * _speedMultiplier);
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
            _thrusterMain.SetActive(true);
            _thrusterSpeed.SetActive(false);
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
        _audioSource.volume = 0.3f;
    }


    public void Damage()
    {
        if (_isShieldPowerUpActive == true)
        {
            _shieldHealth -= 1;

            if (_shieldHealth == 1)
            {
                _shieldColor.material.color = _colorTwo;
            }

            if (_shieldHealth == 0)
            {
                _shieldColor.material.color = _originalColor;
                _isShieldPowerUpActive = false;
                Invincible();
                return;
            }
        }

        if (_isShieldPowerUpActive == false)
        {
            _playerLives -= 1;
            Invincible();
        }

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy Laser")
        {

            Damage();
            Instantiate(_tinyExplosionPrefab, transform.position, Quaternion.identity);

            Destroy(other.gameObject);
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
            yield return new WaitForSeconds(10);
            _isSpeedPowerUpActive = false;
        }
    }



    public void ShieldActive()
    {
        _isShieldPowerUpActive = true;
        _shieldColor.material.color = _colorOne;
        _shieldHealth = 2;
    }

    public void AddToScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    public void SubtractFromScore(int points)
    {
        _score -= points;
        _uiManager.UpdateScore(_score);
    }

    void Invincible()
    {
        _damageTaken = true;
        StartCoroutine(DamageDelay());
        StartCoroutine(SpriteFlicker());
        
    }

    IEnumerator DamageDelay()
    {
        while (_damageTaken == true)
        {
            _collider.enabled = false;
            yield return new WaitForSeconds(2);
            _collider.enabled = true;
            _damageTaken = false;
        }
    }

    IEnumerator SpriteFlicker()
    {
        while (_damageTaken == true)
        {
            _spriteRenderer.enabled = false;
            _leftThruster.SetActive(false);
            _rightThruster.SetActive(false);
                   
            yield return _waitForSeconds02;

            _spriteRenderer.enabled = true;
            _leftThruster.SetActive(true);
            _rightThruster.SetActive(true);
            yield return _waitForSeconds02;
        }
    }






}













