using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver;
    
    private bool _canPress;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true && _canPress == true)
        {
            SceneManager.LoadScene(0); //Current Game Scene
        }
    }

   
    public void GameOver()
    {
        _isGameOver = true;
    }

    public void PressRToRestart ()
    {
        _canPress = true;
    }
}
