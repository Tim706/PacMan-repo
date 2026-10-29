using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject screenBlocker;
    public TextMeshProUGUI gameOverText;
    public Button exitButton;

    [Header("References")]
    public PacStudentController pacStudent;
    public CherryController cherryController;
    public GameTimerScript gameTimer;

    private bool hasTriggered = false;

    public void TriggerGameOver()
    {
        if (hasTriggered) return;
        hasTriggered = true;

        StartCoroutine(GameOverSequence());
    }

    IEnumerator GameOverSequence()
    {
        pacStudent.canMove = false;
        cherryController.canSpawn = false;
        gameTimer.StopTimer();
        exitButton.interactable = false;

        screenBlocker.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = "Game Over";

        SaveHighScoreIfBetter();

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("StartScene");
    }

    void SaveHighScoreIfBetter()
    {
        int currentScore = GameManager.Instance.GetScore();
        float currentTime = gameTimer.GetElapsedTime();

        int savedHighScore = PlayerPrefs.GetInt("Level1_HighScore", 0);
        float savedBestTime = PlayerPrefs.GetFloat("Level1_BestTime", float.MaxValue);

        bool isNewHighScore = currentScore > savedHighScore || (currentScore == savedHighScore && currentTime < savedBestTime);

        if (isNewHighScore)
        {
            PlayerPrefs.SetInt("Level1_HighScore", currentScore);
            PlayerPrefs.SetFloat("Level1_BestTime", currentTime);
            PlayerPrefs.Save();
        }
    }
    
    void Start()
    {
    
    }

    void Update()
    {
        
    }
}


