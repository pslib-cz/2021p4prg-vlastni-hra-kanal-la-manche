using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour
{
    public GameObject startMenuPanel;
    public GameObject howToPlayPanel;
    public GameObject difficultyPanel;
    public GameObject playersPanel;
    public GameObject highScoresPanel;
    public Text highScoresText;
    public Text coopHighScoresText;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("HIGHSCORENAME") != "")
        {
            highScoresText.text = "Single Player High Score is " + PlayerPrefs.GetInt("HIGHSCORE") + " by " + PlayerPrefs.GetString("HIGHSCORENAME");   
        }
        if (PlayerPrefs.GetString("HIGHSCORECOOPNAME") != "")
        {
            coopHighScoresText.text = "Co-op High Score is " + PlayerPrefs.GetInt("HIGHSCORECOOP") + " by " + PlayerPrefs.GetString("HIGHSCORECOOPNAME");   
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Exiting game");
    }
    public void StartGame()
    {
        startMenuPanel.SetActive(false);
        playersPanel.SetActive(true);
    }
    public void HowToPlay()
    {
        startMenuPanel.SetActive(false);
        howToPlayPanel.SetActive(true);        
    }
    public void HighScores()
    {
        startMenuPanel.SetActive(false);
        highScoresPanel.SetActive(true);
    }
    public void BackToMenu()
    {
        startMenuPanel.SetActive(true);
        howToPlayPanel.SetActive(false);
        playersPanel.SetActive(false);
        difficultyPanel.SetActive(false);       
        highScoresPanel.SetActive(false); 
    }
    public void SinglePlayer()
    {
        PlayerPrefs.SetInt("COOP", 0);
        playersPanel.SetActive(false);
        difficultyPanel.SetActive(true);
    }
    public void CoopPlayer()
    {
        PlayerPrefs.SetInt("COOP", 1);
        playersPanel.SetActive(false);
        difficultyPanel.SetActive(true);
        //PlayerPrefs.SetInt("DIFFICULTY", 3);
        //SceneManager.LoadScene("CoopScene");
    }
    public void Easy()
    {
        PlayerPrefs.SetInt("DIFFICULTY", 1);
        if (PlayerPrefs.GetInt("COOP") == 1)
        {
            SceneManager.LoadScene("CoopScene");
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    public void Normal()
    {
        PlayerPrefs.SetInt("DIFFICULTY", 2);
        if (PlayerPrefs.GetInt("COOP") == 1)
        {
            SceneManager.LoadScene("CoopScene");
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    public void Hard()
    {
        PlayerPrefs.SetInt("DIFFICULTY", 3);
        if (PlayerPrefs.GetInt("COOP") == 1)
        {
            SceneManager.LoadScene("CoopScene");
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
