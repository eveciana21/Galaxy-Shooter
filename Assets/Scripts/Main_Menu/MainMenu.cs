using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private bool _isPressed;
    private float _speed = 15;
    [SerializeField] GameObject _playerShip;
    [SerializeField] GameObject _controlsText;
    private bool _controlsButtonPressed;

    private void Start()
    {
        if (_playerShip != null)
        {
            _playerShip.transform.position = new Vector3(0f, -7.25f, 0);
        }

        _controlsText.SetActive(false);

    }

    private void Update()
    {
        if (_isPressed == true && _playerShip != null)
        {
            LoadGame();
        }
    }
    public void LoadGame()
    {
        if (_playerShip != null)
        {
            _isPressed = true;
            _playerShip.transform.Translate(Vector3.up * _speed * Time.deltaTime);

            if (_playerShip.transform.position.y > 10)
            {
                SceneManager.LoadScene(1); // Main Game Scene
                Destroy(_playerShip);
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ControlScreen()
    {
        _controlsText.SetActive(true);
    }





    





}
