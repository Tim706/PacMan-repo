using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public GhostScaredTimerScript scaredTimerDisplay;

    [Header("Ghosts")]
    public Animator[] ghostAnimators; 

    [Header("Music")]
    public MusicScript musicScript;
    
    private int score = 0;
    private int[] ghostStates; 
    private Coroutine scaredCoroutine;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

        ghostStates = new int[ghostAnimators.Length];
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = score.ToString("D6");
    }

    public void ActivatePowerPill()
    {
        AddScore(50);

        if (scaredCoroutine != null)
        {
            StopCoroutine(scaredCoroutine);
        }

        scaredCoroutine = StartCoroutine(ScaredSequence());
    }

    IEnumerator ScaredSequence()
    {
        SetGhostsState(1); 
        musicScript.goblinIsScared = true;

        float timeRemaining = 10f;
        scaredTimerDisplay.scaredTimeRemaining = Mathf.CeilToInt(timeRemaining);

        bool recoveringTriggered = false;

        while (timeRemaining > 0f)
        {
            timeRemaining -= Time.deltaTime;
            scaredTimerDisplay.scaredTimeRemaining = Mathf.CeilToInt(Mathf.Max(timeRemaining, 0f));

            if (!recoveringTriggered && timeRemaining <= 3f)
            {
                SetGhostsState(2); 
                recoveringTriggered = true;
            }

            yield return null;
        }

        SetGhostsState(0); 
        musicScript.goblinIsScared = false;
        scaredTimerDisplay.scaredTimeRemaining = 0;
        scaredCoroutine = null;
    }

    void SetGhostsState(int state)
    {
        for (int i = 0; i < ghostAnimators.Length; i++)
        {
            if (ghostStates[i] == 3) continue;
            ghostStates[i] = state;
            ghostAnimators[i].SetInteger("Ghost State", state);
        }
    }
}