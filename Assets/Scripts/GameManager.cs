using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver;

    private bool _canPress;

    private bool _escapePressed;

    [SerializeField] private GameObject _endGameText;

    private void Start()
    {
        _endGameText.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true && _canPress == true)
        {
            SceneManager.LoadScene(1); //Current Game Scene 
        }

        if (Input.GetKeyDown(KeyCode.Escape))// && _isGameOver == false)
        {
            _endGameText.SetActive(true);
            Time.timeScale = 0;
            _escapePressed = true;
        }
        if (Input.GetKeyDown(KeyCode.Y) && _escapePressed == true)
        {
            SceneManager.LoadScene(0); //Main Menu
            Time.timeScale = 1;
            _escapePressed = false;
        }
        else if (Input.GetKeyDown(KeyCode.N) && _escapePressed == true)
        {
            _endGameText.SetActive(false);
            Time.timeScale = 1;
            _escapePressed = false;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0); //Main Menu
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void PressRToRestart()
    {
        _canPress = true;
    }



}
