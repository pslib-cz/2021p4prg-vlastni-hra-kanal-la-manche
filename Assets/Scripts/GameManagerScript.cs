using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public int lives;
    public int score;
    public Text livesText;
    public Text scoreText;
    public Text highScoreText;
    public Text differenceText;
    public InputField highScoreInput;
    public bool gameOver;
    public bool newLevel;
    public GameObject gameOverPanel;
    public GameObject newLevelPanel;
    public int numberOfBricks;
    public Transform[] levels;
    public int currentLevelIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentLevelIndex = 0;
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
        numberOfBricks = GetAllBricks();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateNumberOfBricks();
    }

    public void UpdateLives(int changeInLives)
    {
        lives += changeInLives;

        if (lives <= 0)
        {
            lives = 0;
            GameOver();
        }

        livesText.text = "Lives: " + lives;
    }
    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }
    private int GetAllBricks()
    {
        int helper = 0;
        helper = GameObject.FindGameObjectsWithTag("NoCollisionBrick").Length;
        helper += GameObject.FindGameObjectsWithTag("brick").Length;
        helper += GameObject.FindGameObjectsWithTag("redBrick").Length;

        return helper;
    }
    private void UpdateNumberOfBricks()
    {
        numberOfBricks = GetAllBricks();        
    }
    public void CheckLevel()
    {
        if (numberOfBricks <= 0)
        {
            if (currentLevelIndex >= levels.Length - 1)
            {
                GameOver();   
            }
            else
            {
                gameOver = true;
                newLevelPanel.SetActive(true);
                newLevelPanel.GetComponentInChildren<Text>().text = "Level " + (currentLevelIndex + 2);
                StartCoroutine(LoadLevelDelayed());
            }
        }
        Debug.Log(numberOfBricks);
    }

    private void LoadLevel()
    {
        currentLevelIndex++;
        Instantiate(levels[currentLevelIndex], Vector2.zero, Quaternion.identity);
        numberOfBricks = GetAllBricks();
        gameOver = false;
        newLevelPanel.SetActive(false);
    }

    public void GameOver(){
        gameOver = true;
        gameOverPanel.SetActive(true);
        int highScore = PlayerPrefs.GetInt("HIGHSCORE");
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HIGHSCORE", score);
            highScoreText.text = "New High Score! " + score;
            differenceText.text = "Enter your name below";
            highScoreInput.gameObject.SetActive(true);
        }
        else
        {
            highScoreText.text = PlayerPrefs.GetString("HIGHSCORENAME") + "'s High Score is " + highScore;
            differenceText.text = "You needed just " + CalculateScoreDif(highScore) + " points to beat it!";
        }
    }
    public void NewHighScore()
    {
        string highScoreName = highScoreInput.text;
        PlayerPrefs.SetString("HIGHSCORENAME", highScoreName);
        highScoreInput.gameObject.SetActive(false);
        highScoreText.text = "New High Score! " + score;
        differenceText.text = "Congratulations " + highScoreName;
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
    IEnumerator LoadLevelDelayed()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);

        LoadLevel();
    }
    private int CalculateScoreDif(int highscore)
    {
        int dif = highscore - score;
        return dif;
    }
}
