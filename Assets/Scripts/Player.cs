using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _speedMultiplier = 1.8f;

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

    [SerializeField] private GameObject _thrusterSpeed;
    [SerializeField] private GameObject _thrusterMain;
    [SerializeField] private GameObject _leftThruster, _rightThruster;


    [SerializeField] private GameObject _leftSmoke, _rightSmoke;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private GameObject _tinyExplosionPrefab;


    private int _score;
    private int _highScore;
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

    private int _ammo = 50;
    private bool _noAmmoLeft;
    [SerializeField] private bool _trySpawningAmmo;


    [SerializeField] private GameObject _fighterBrigadePrefab;
    [SerializeField] private bool _fighterBrigadeActive;

    [SerializeField] private bool _heatSeakingMissilesActive;
    [SerializeField] private GameObject _missile;

    private float _boostRemaining;
    private bool _thrusterEngaged;
    [SerializeField] private float _refuelSpeed = 20f;

    [SerializeField] private GameObject _speedBoostParticleSystem;

    private CameraShake _cameraShake;

    [SerializeField] private int _currentKillCount;

    private bool _negativePowerupActive;

    [SerializeField] private bool _isPlayerAlive;

    [SerializeField] AudioSource _gameMusic;

    [SerializeField] private GameObject _greenExplosion;

    void Start()
    {
        _thrusterSpeed.SetActive(false);
        _leftSmoke.SetActive(false);
        _rightSmoke.SetActive(false);
        _speedBoostParticleSystem.SetActive(false);

        transform.position = new Vector3(0, -2.5f, 0);

        //<GET COMPONENT>
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _audioSource = GetComponent<AudioSource>();
        _turnAnim = gameObject.GetComponent<Animator>();
        _shieldColor = GetComponent<Renderer>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();

        //NULL CHECKS
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

        _isPlayerAlive = true;

        _highScore = PlayerPrefs.GetInt("_highScore", _highScore);
        _uiManager.HighScore(_highScore);

    }

    void Update()
    {
        ControlMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire && _noAmmoLeft == false)
        {
            _ammo--;
            FireLaser();
        }

        AmmoLimits();

        if (Input.GetKeyDown(KeyCode.P) && _fighterBrigadeActive == true)
        {
            Instantiate(_fighterBrigadePrefab, new Vector3(0, -9, 0), Quaternion.identity);
            _fighterBrigadeActive = false;
            _uiManager.FighterBrigadeNotActive();
        }


    }
    private void OnDestroy()
    {
        PlayerPrefs.SetInt("_highScore", _highScore);
        PlayerPrefs.Save();
    }

    void ControlMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        //transform.Translate(Vector3.right * horizontal * _speed * Time.deltaTime);
        float vertical = Input.GetAxis("Vertical");
        //transform.Translate(Vector3.up * vertical * _speed * Time.deltaTime);

        Vector3 direction = new Vector3(horizontal, vertical, 0);

        //TURNING ANIMATION
        if (horizontal < 0 && _negativePowerupActive == false)
        {
            _turnAnim.SetBool("Left_Turn_anim", true);
            _turnAnim.SetBool("Right_Turn_anim", false);
        }
        else if (horizontal > 0 && _negativePowerupActive == false)
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
        if (Input.GetKey(KeyCode.J) && _isSpeedPowerUpActive == true && _damageTaken == false && _thrusterEngaged == true && _boostRemaining >= 1 && _negativePowerupActive == false)
        {
            SpeedBoostSliderDecrease();

            transform.Translate(direction * _speed * _speedMultiplier * Time.deltaTime);
            _thrusterSpeed.SetActive(true);
            _thrusterMain.SetActive(false);
            _speedBoostParticleSystem.SetActive(true);

            Debug.Log("Speed Multiplier: " + _speed * _speedMultiplier);
        }

        else if (_negativePowerupActive == true)
        {
            transform.Translate(direction * _speed / 3 * Time.deltaTime);
        }

        else
        {
            if (_isSpeedPowerUpActive == true)
            {
                SpeedBoostSliderIncrease();
            }

            transform.Translate(direction * _speed * Time.deltaTime);

            _thrusterSpeed.SetActive(false);
            _speedBoostParticleSystem.SetActive(false);
        }



        //X position
        if (transform.position.x > 10.75f)
        {
            transform.position = new Vector3(-10.75f, transform.position.y, 0);
        }
        else if (transform.position.x < -10.75f)
        {
            transform.position = new Vector3(10.75f, transform.position.y, 0);
        }

        //Y position
        if (_isSpeedPowerUpActive == true)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.05f, 5.75f), 0);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.50f, 5.75f), 0);
        }
    }

    void FireLaser()
    {
        _uiManager.AmmoCount(_ammo);
        _uiManager.AmmoSlider(_ammo);


        _canFire = Time.time + _fireRate;

        if (_tripleShotIsActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else if (_heatSeakingMissilesActive == true)
        {
            Instantiate(_missile, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }

        _audioSource.Play();
        _audioSource.volume = 0.2f;
    }

    public void HeatSeakingMissiles()
    {
        _heatSeakingMissilesActive = true;
    }

    void AmmoLimits()
    {
        if (_ammo <= 10)
        {
            if (_trySpawningAmmo == false)
            {
                _spawnManager.StartSpawningAmmo();
                _trySpawningAmmo = true;
            }
            _uiManager.LowAmmo();
            _noAmmoLeft = false;
        }

        else if (_ammo > 10)
        {
            _trySpawningAmmo = false;
            _spawnManager.EnoughAmmo();

            _uiManager.EnoughAmmo();
            _noAmmoLeft = false;
        }

        if (_ammo < 1)
        {
            _uiManager.NoAmmo();
            _noAmmoLeft = true;
        }
    }

    public void AmmoPickup()
    {
        _ammo += 50;
        _uiManager.AmmoCount(_ammo);
        _uiManager.AmmoSlider(_ammo);

        if (_ammo >= 50)
        {
            _ammo = 50;
            _uiManager.AmmoCount(_ammo);
        }
    }


    public void Damage()
    {
        if (_isShieldPowerUpActive == true && _damageTaken == false)
        {
            _shieldHealth -= 1;
            Invincible();
            CameraShake();

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

        if (_isShieldPowerUpActive == false && _damageTaken == false)
        {
            _playerLives -= 1;
            CameraShake();
            Invincible();
        }

        if (_playerLives == 2)
        {
            _leftSmoke.SetActive(true);
        }
        else if (_playerLives == 1)
        {
            _rightSmoke.SetActive(true);
            _uiManager.DangerUI();
        }

        _uiManager.UpdateLives(_playerLives);


        if (_playerLives < 1)
        {
            _isPlayerAlive = false;
            HighScoreText();
            _spawnManager.PlayerDead();
            _uiManager.DangerUIStop();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void CameraShake()
    {
        _cameraShake.CameraShaking();
    }

    public void HealthPickup()
    {
        if (_playerLives < 3)
        {
            _playerLives++;
            _uiManager.DangerUIStop();

            _uiManager.UpdateLives(_playerLives);

            if (_playerLives == 2)
            {
                _rightSmoke.SetActive(false);
            }
            else if (_playerLives == 3)
            {
                _leftSmoke.SetActive(false);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy Laser")
        {
            if (_damageTaken == false)
            {
                Damage();
                Instantiate(_tinyExplosionPrefab, transform.position, Quaternion.identity);

                Destroy(other.gameObject);
            }
        }

        if (other.tag == "Enemy")
        {
            if (_damageTaken == false)
            {
                SubtractFromScore(50);
                Damage();

                Instantiate(_explosionPrefab, other.transform.position, Quaternion.identity);
                CurrentKillCount();
                Destroy(other.gameObject);
            }
        }
        if (other.tag == "Alien Enemy")
        {
            SubtractFromScore(50);
            Damage();
            Instantiate(_greenExplosion, transform.position, Quaternion.identity);
            CurrentKillCount();
            Destroy(other.gameObject);
        }
        if (other.tag == "Green Slime")
        {
            if (_damageTaken == false)
            {
                Damage();

                Instantiate(_greenExplosion, transform.position, Quaternion.identity);
                Destroy(other.gameObject);
            }
        }
        if (other.tag == "Explosion")
        {
            if (_damageTaken==false)
            {
                Damage();
                Destroy(other.gameObject, 2f);
            }
        }
    }

    public void CurrentKillCount()
    {
        _currentKillCount++;
        Debug.Log("Current Kill Count: " + _currentKillCount);

        if (_currentKillCount == 10 && _isPlayerAlive == true)
        {
            _spawnManager.WaveTwo();
            _uiManager.WaveTwoUI();
        }
        else if (_currentKillCount == 20 && _isPlayerAlive == true)
        {
            _spawnManager.WaveThree();
            _uiManager.WaveThreeUI();
        }
        else if (_currentKillCount == 30 && _isPlayerAlive == true)
        {
            _spawnManager.WaveFour();
            _uiManager.WaveFourUI();
        }
        else if (_currentKillCount == 40 && _isPlayerAlive == true)
        {
            _spawnManager.WaveFive();
            _uiManager.WaveFiveUI();
        }
    }



    public void NegativePowerupSlow()
    {
        _negativePowerupActive = true;

        if (_negativePowerupActive == true)
        {
            StartCoroutine(DecreaseGamePitch());
        }

        StartCoroutine(ThrusterFlicker());
    }

    IEnumerator DecreaseGamePitch()
    {
        while (_negativePowerupActive == true)
        {
            _gameMusic.pitch -= 0.05f;

            if (_gameMusic.pitch <= 0.7f)
            {
                _gameMusic.pitch = 0.7f;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator IncreaseGamePitch()
    {
        while (_gameMusic.pitch < 1f)
        {
            _gameMusic.pitch += 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator ThrusterFlicker()
    {
        _thrusterMain.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        _thrusterMain.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _thrusterMain.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        _thrusterMain.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _thrusterMain.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        _thrusterMain.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        _thrusterMain.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        _thrusterMain.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        _thrusterMain.SetActive(false);

        StartCoroutine(NegativePowerupCooldown());
    }
    IEnumerator NegativePowerupCooldown()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine(ThrusterFlickerEngaging());
    }

    IEnumerator ThrusterFlickerEngaging()
    {
        _thrusterMain.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _thrusterMain.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        _thrusterMain.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _thrusterMain.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        _thrusterMain.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        _thrusterMain.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        _thrusterMain.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        _thrusterMain.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        _thrusterMain.SetActive(true);

        _negativePowerupActive = false;

        StartCoroutine(IncreaseGamePitch());
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

    public void FighterBrigadePowerup()
    {
        _fighterBrigadeActive = true;
        _uiManager.FighterBrigadePowerup();
    }


    public void SpeedBoostActive()
    {
        _isSpeedPowerUpActive = true;
        _uiManager.PressShiftToBoost();
        StopCoroutine("SpeedBoostActiveTime");
        StartCoroutine("SpeedBoostActiveTime");
    }

    IEnumerator SpeedBoostActiveTime()
    {
        if (_isSpeedPowerUpActive == true)
        {
            _thrusterEngaged = true;
            yield return new WaitForSeconds(20f);
            _isSpeedPowerUpActive = false;
            _uiManager.BoostSlider(_boostRemaining = 0);
        }
    }

    void SpeedBoostSliderIncrease()
    {
        _uiManager.BoostSlider(_boostRemaining += Time.deltaTime * _refuelSpeed);
        if (_boostRemaining >= 100)
        {
            _boostRemaining = 100f;
        }
    }


    void SpeedBoostSliderDecrease()
    {
        _uiManager.BoostSlider(_boostRemaining -= Time.deltaTime * _refuelSpeed);
        if (_boostRemaining <= 1)
        {
            _thrusterEngaged = false;
            StartCoroutine(SpeedBoostCooldown());
        }
    }

    IEnumerator SpeedBoostCooldown()
    {
        yield return new WaitForSeconds(1f);
        _thrusterEngaged = true;
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

        if (_score <= 0)
        {
            _uiManager.UpdateScore(_score = 0);
        }
    }

    private void HighScoreText()
    {
        if (_score > _highScore)
        {
            _highScore = _score;
            Debug.Log("High Score " + _highScore);
            _uiManager.HighScore(_highScore);
        }
    }

    public void Invincible()
    {
        _damageTaken = true;
        StartCoroutine(DamageDelay());
        StartCoroutine(SpriteFlicker());
    }

    IEnumerator DamageDelay()
    {
        while (_damageTaken == true)
        {
            yield return new WaitForSeconds(1f);
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













