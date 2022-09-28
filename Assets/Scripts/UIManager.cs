using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    
    
    private Player _player;


    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _scoreText.text = "Score: " + 0;
    }

    public void UpdateScore (int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
        Debug.Log("50 " + playerScore);
    }




}
