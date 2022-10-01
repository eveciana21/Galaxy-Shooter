using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;

    [SerializeField] private Sprite [] _livesSprite;

    [SerializeField] private Image _livesDisplayImage;

    [SerializeField] private Text _gameOverText;
    [SerializeField] private Text _restartLevel;

    private WaitForSeconds _waitForSeconds = new WaitForSeconds(0.5f);
    private GameManager _gameManager;




    void Start()
    {
        _scoreText.text = "Score: " + 0;

        _gameOverText.gameObject.SetActive(false);
        _restartLevel.gameObject.SetActive(false);

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    public void UpdateScore (int playerScore)
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

    IEnumerator RestartLevel ()
    {
        yield return new WaitForSeconds(1.75f);
        _restartLevel.gameObject.SetActive(true);
        _gameManager.PressRToRestart();
    }







}
