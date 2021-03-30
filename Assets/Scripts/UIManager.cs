using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int _score = 0;
    private int _lives = 5;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _livesText;
    [SerializeField] private Text _gameoverText;


    private void Start()
    {
        _scoreText.text = "Score: " + _score;
        _livesText.text = "Lives: " + _lives;
        _gameoverText.gameObject.SetActive(false);
    }

    public void ShowGameOver()
    {
        _gameoverText.gameObject.SetActive(true);
        _gameoverText.gameObject.SetActive(true);
    }

    public void AddScore(int score)
    {
        _score += score;
        _scoreText.text = "Score: " + _score;
    }

    public void CountLives(int lives)
    {
        _lives -= lives;
        _livesText.text = "Lives: " + _lives;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
