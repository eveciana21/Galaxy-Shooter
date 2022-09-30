using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Sprite [] _livesSprite;
    [SerializeField] private Image _livesDisplayImage;
    
    


    void Start()
    {
        _scoreText.text = "Score: " + 0;
    }

    public void UpdateScore (int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();       
    }

    public void UpdateLives(int currentLives)
    {

        _livesDisplayImage.sprite = _livesSprite[currentLives];


    }



}
