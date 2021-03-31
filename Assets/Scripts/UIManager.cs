using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // initializes score and live
    private int _score = 0;
    [Range(0,20)]
    private int _lives = 5;
    
    // import the different texts
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _livesText;
    [SerializeField] private Text _gameoverText;



    private void Start()
    {
        // at the beginning display inital score/lives and make gameovertext inactive
        _scoreText.text = "Score: " + _score;
        _livesText.text = "Lives: " + _lives;
        _gameoverText.gameObject.SetActive(false);
    }

    public void ShowGameOver()
    {
        // if game is over, this function is called and sets the gameovertext as active
        _gameoverText.gameObject.SetActive(true);
        _lives = 0;
    }

    public void AddScore(int score)
    {
        // when this function is called, the given int is added to the score and the changes is displayed in the text
        _score += score;
        _scoreText.text = "Score: " + _score;
    }

    public void CountLives(int lives)
    {
        // when this function is called, the given int is subtracted to the score and the changes is displayed in the text
        _lives -= lives;
        _livesText.text = "Lives: " + _lives;

    }
}
