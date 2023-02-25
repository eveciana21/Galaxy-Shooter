using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;

    [SerializeField] private Sprite[] _livesSprite;

    [SerializeField] private Image _livesDisplayImage;

    [SerializeField] private Text _gameOverText;
    [SerializeField] private Text _restartLevel;

    [SerializeField] private Text _ammoCount;

    private WaitForSeconds _waitForSeconds = new WaitForSeconds(0.5f);
    private GameManager _gameManager;

    [SerializeField] private Slider _boostSlider;

    [SerializeField] private Slider _ammoSlider;

    [SerializeField] private Text[] _waves;

    [SerializeField] GameObject _dangerUI;

    [SerializeField] private Text _highScore;
    [SerializeField] private Text _highScoreValue;

    [SerializeField] private Slider _bossHealthSlider;

    [SerializeField] private Slider _circleSlider;

    [SerializeField] private Slider _tripleShotSlider;

    [SerializeField] private GameObject _creditsAnim;

    [SerializeField] private Image _magnet;

    [SerializeField] private GameObject _starsParticle;

    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        _scoreText.text = "Score: " + 0;

        _gameOverText.gameObject.SetActive(false);
        _restartLevel.gameObject.SetActive(false);
        _dangerUI.gameObject.SetActive(false);
        _highScore.gameObject.SetActive(false);
        _bossHealthSlider.gameObject.SetActive(false);
        _circleSlider.gameObject.SetActive(false);
        _creditsAnim.gameObject.SetActive(false);
        _tripleShotSlider.gameObject.SetActive(false);
        _magnet.gameObject.SetActive(false);
        _starsParticle.gameObject.SetActive(false);

        _tripleShotSlider.value = 100f;
        _circleSlider.value = 100f;
        _boostSlider.value = 0;
        _ammoSlider.value = 100;

        WavesStartPos();
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void HighScore(int playerHighScore)
    {
        _highScore.gameObject.SetActive(true);
        _highScoreValue.text = playerHighScore.ToString();
        StartCoroutine(HighScoreFlicker());
    }

    IEnumerator HighScoreFlicker()
    {
        while (true)
        {
            _highScoreValue.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _highScoreValue.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void UpdateLives(int currentLives)
    {
        _livesDisplayImage.sprite = _livesSprite[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        StartCoroutine(GameOverFlicker());
        StartCoroutine(RestartLevel());
    }
    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return _waitForSeconds;
            _gameOverText.gameObject.SetActive(false);
            yield return _waitForSeconds;
        }
    }
    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1.75f);
        _restartLevel.gameObject.SetActive(true);
        _gameManager.PressRToRestart();
    }

    public void AmmoCount(int currentAmmo)
    {
        _ammoCount.text = "Ammo: " + currentAmmo.ToString() + "/100";
    }

    public void AmmoSlider(int ammoCount)
    {
        _ammoSlider.value = ammoCount;

        _ammoSlider.minValue = 0;
        _ammoSlider.maxValue = 100;
    }

    public void NoAmmo()
    {
        _ammoCount.color = Color.red;
    }
    public void LowAmmo()
    {
        _ammoCount.color = Color.yellow;
    }
    public void EnoughAmmo()
    {
        _ammoCount.color = Color.white;
    }

    public void TripleShotCooldown(float secondsRemaining)
    {
        _tripleShotSlider.gameObject.SetActive(true);

        _tripleShotSlider.value = secondsRemaining;

        _tripleShotSlider.maxValue = 100f;
        _tripleShotSlider.minValue = 0f;
    }

    public void TripleShotTimerComplete()
    {
        _tripleShotSlider.gameObject.SetActive(false);
    }

    public void HomingMissileCircleSlider(float timeRemaining)
    {
        _circleSlider.gameObject.SetActive(true);

        _circleSlider.value = timeRemaining;

        _circleSlider.maxValue = 100f;
        _circleSlider.minValue = 0f;
    }

    public void CircleSliderCompleted()
    {
        _circleSlider.gameObject.SetActive(false);
    }

    public void BoostSlider(float boostPercent)
    {
        _boostSlider.value = boostPercent;

        _boostSlider.maxValue = 100f;
        _boostSlider.minValue = 0f;
    }

    public void MagnetActive()
    {
        _magnet.gameObject.SetActive(true);
    }
    public void MagnetNotActive()
    {
        _magnet.gameObject.SetActive(false);
    }

    private void WavesStartPos()
    {
        for (int i = 0; i < _waves.Length; i++)
        {
            _waves[i].gameObject.SetActive(false);
        }
    }

    public void WaveOneUI()
    {
        _waves[0].gameObject.SetActive(true);
        StartCoroutine(WaveOnScreenTime());
    }
    public void WaveTwoUI()
    {
        _waves[1].gameObject.SetActive(true);
        StartCoroutine(WaveOnScreenTime());
    }
    public void WaveThreeUI()
    {
        _waves[2].gameObject.SetActive(true);
        StartCoroutine(WaveOnScreenTime());
    }
    public void WaveFourUI()
    {
        _waves[3].gameObject.SetActive(true);
        StartCoroutine(WaveOnScreenTime());
    }
    public void WaveFiveUI()
    {
        _waves[4].gameObject.SetActive(true);
        StartCoroutine(WaveOnScreenTime());
    }

    IEnumerator WaveOnScreenTime()
    {
        yield return new WaitForSeconds(5);

        for (int i = 0; i < _waves.Length; i++)
        {
            _waves[i].gameObject.SetActive(false);
        }
    }

    public void DangerUI()
    {
        StartCoroutine("DangerFlicker");
    }
    public void DangerUIStop()
    {
        StopCoroutine("DangerFlicker");
        _dangerUI.SetActive(false);
    }

    IEnumerator DangerFlicker()
    {
        while (true)
        {
            _dangerUI.SetActive(true);
            yield return new WaitForSeconds(0.4f);
            _dangerUI.SetActive(false);
            yield return new WaitForSeconds(0.4f);
        }
    }

    public void BossHealthSlider(float damageTaken)
    {
        _bossHealthSlider.gameObject.SetActive(true);
        _bossHealthSlider.value = damageTaken;

        _bossHealthSlider.minValue = 0;
        _bossHealthSlider.maxValue = 100;
    }

    public void BossDead()
    {
        _bossHealthSlider.gameObject.SetActive(false);
        StartCoroutine(CreditsDelayTimer());

    }
    IEnumerator CreditsDelayTimer()
    {
        yield return new WaitForSeconds(9);
        _creditsAnim.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        _starsParticle.gameObject.SetActive(true);
        StartCoroutine(CreditsEnd());
    }

    IEnumerator CreditsEnd()
    {
        yield return new WaitForSeconds(15);
        _creditsAnim.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        _starsParticle.gameObject.SetActive(false);
        _gameManager.MainMenu();
    }
}