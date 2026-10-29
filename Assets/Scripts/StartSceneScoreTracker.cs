using UnityEngine;
using TMPro;

public class StartSceneScoreTracker : MonoBehaviour
{
    
    public TextMeshProUGUI level1HighScoreText;
    public TextMeshProUGUI level1BestTimeText;

    void Start()
    {
        int highScore = PlayerPrefs.GetInt("Level1_HighScore", 0);
        float bestTime = PlayerPrefs.GetFloat("Level1_BestTime", 0f);

        level1HighScoreText.text = highScore.ToString("D6");
        level1BestTimeText.text = FormatTime(bestTime);
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int fraction = Mathf.FloorToInt((time % 1f) * 100f);
        return $"{minutes:D2}:{seconds:D2}:{fraction:D2}";
    }
    
    void Update()
    {
        
    }
}
