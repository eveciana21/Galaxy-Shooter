using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;

    [SerializeField] private Sprite[] _livesSprite;

    [SerializeField] private Image _livesDisplayImage;

    [SerializeField] private Text _gameOverText;
    [SerializeField] private Text _restartLevel;

    [SerializeField] private Text _ammoCount;

    [SerializeField] private Text _pressP;
    [SerializeField] private Image _fighterBrigade;

    [SerializeField] private Text _pressShift;
    private bool _speedBoostActive;

    private WaitForSeconds _waitForSeconds = new WaitForSeconds(0.5f);
    private GameManager _gameManager;

    [SerializeField] private Slider _boostSlider;

    [SerializeField] private Slider _ammoSlider;

    void Start()
    {
        _scoreText.text = "Score: " + 0;

        _gameOverText.gameObject.SetActive(false);
        _restartLevel.gameObject.SetActive(false);
        _fighterBrigade.gameObject.SetActive(false);
        _pressShift.gameObject.SetActive(false);

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        Text _ammoText = _ammoCount.GetComponent<Text>();

        _boostSlider.value = 0;
        _ammoSlider.value = 50;

    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
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

    public void FighterBrigadePowerup()
    {
        _fighterBrigade.gameObject.SetActive(true);
        StartCoroutine(PressPFlicker());
    }
    IEnumerator PressPFlicker()
    {
        while (true)
        {
            _pressP.gameObject.SetActive(true);
            yield return _waitForSeconds;
            _pressP.gameObject.SetActive(false);
            yield return _waitForSeconds;
        }
    }

    public void FighterBrigadeNotActive()
    {
        _fighterBrigade.gameObject.SetActive(false);
    }

    public void PressShiftToBoost()
    {
        _speedBoostActive = true;

        if (_speedBoostActive == true)
        {
            StartCoroutine(PressShiftFlicker());
        }
    }

    IEnumerator PressShiftFlicker()
    {
        _pressShift.gameObject.SetActive(true);
        yield return _waitForSeconds;
        _pressShift.gameObject.SetActive(false);
        yield return _waitForSeconds;
        _pressShift.gameObject.SetActive(true);
        yield return _waitForSeconds;
        _pressShift.gameObject.SetActive(false);
        yield return _waitForSeconds;
        _pressShift.gameObject.SetActive(true);
        yield return _waitForSeconds;
        _pressShift.gameObject.SetActive(false);
        yield return _waitForSeconds;
        _speedBoostActive = false;
    }

    public void AmmoCount(int currentAmmo)
    {
        _ammoCount.text = "Ammo: " + currentAmmo.ToString() + "/50";
    }

    public void AmmoSlider(int ammoCount)
    {
        _ammoSlider.value = ammoCount;

        _ammoSlider.minValue = 0;
        _ammoSlider.maxValue = 50;
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


    public void BoostSlider(float boostPercent)
    {
        _boostSlider.value = boostPercent;

        _boostSlider.maxValue = 100f;
        _boostSlider.minValue = 0f;
    }











}
