using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string highScoreKey;

    [Header("Components")]
    [SerializeField] private Rigidbody2D playerRg2D;
    [SerializeField] private GameObject player, gameOverCanvas, gamePlayCanvas, textStartTap;
    [SerializeField] private Slider m_slider;

    [SerializeField] private TextMeshProUGUI inGameScore, finalScore, bestScore;
    private int scoreValue;

    void Start()
    {
        gameOverCanvas.SetActive(false);
        gamePlayCanvas.SetActive(true);
        textStartTap.SetActive(true);
        playerRg2D.bodyType = RigidbodyType2D.Static;
        Time.timeScale = 1;
    }

    void FixedUpdate()
    {
        StartGame();
        InGameScore();
    }

    private void StartGame()
    {
        if (Input.GetMouseButtonDown(0) || Input.anyKey)
        {
            playerRg2D.bodyType = RigidbodyType2D.Dynamic;
            textStartTap.SetActive(false);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverCanvas.SetActive(true);
        gamePlayCanvas.SetActive(false);
        BestScore();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        gameOverCanvas.SetActive(false);
    }

    public void InGameScore()
    {
        scoreValue = (int)player.transform.position.x/10;
        inGameScore.text = "Score: " + scoreValue;
        m_slider.value = scoreValue;
    }

    public void BestScore()
    {
        int highScore = PlayerPrefs.GetInt(highScoreKey);
        if (scoreValue > highScore)
        {
            highScore = scoreValue;
            PlayerPrefs.SetInt(highScoreKey, highScore);
            finalScore.text = "Score: " + scoreValue;
            bestScore.text = "Best: " + highScore;
        }
        else
        {
            finalScore.text = "Score: " + scoreValue;
            bestScore.text = "Best: " + highScore;
        }
    }
}
