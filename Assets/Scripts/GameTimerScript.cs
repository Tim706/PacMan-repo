using UnityEngine;
using TMPro;

public class GameTimerScript : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    private float elapsedTime = 0f;
    private bool isRunning = false;

    void Start()
    {
        UpdateDisplay();
    }

    void Update()
    {
        if (!isRunning) return;

        elapsedTime += Time.deltaTime;
        UpdateDisplay();
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int fraction = Mathf.FloorToInt((elapsedTime % 1f) * 100f);

        timerText.text = $"{minutes:D2}:{seconds:D2}:{fraction:D2}";
    }

    public float GetElapsedTime() => elapsedTime;
}
