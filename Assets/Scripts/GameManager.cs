using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public GhostScaredTimerScript scaredTimerDisplay;
	public GameObject[] healthBar;
	public GameOverManager gameOverManager;

    [Header("Ghosts")]
    public GhostCollision[] ghosts; 

    [Header("Music")]
    public MusicScript musicScript;
	public SoundEffectsScript soundEffects;

	[Header("Player")]
	public PacStudentController pacStudent;
	public ParticleSystem deathParticle;
    
    private int score = 0;
	private int health = 3;
    private Coroutine scaredCoroutine;

	private int totalPellets;
	private int pelletsEaten = 0;

	private bool scaredActive = false;
	private bool recoveringActive = false;

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
        
		totalPellets = CountTotalPellets();
        UpdateScoreUI();
		UpdateHealthUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

	public void PelletsEatenTracker()
	{
		pelletsEaten++;
		if (pelletsEaten >= totalPellets)
		{
			gameOverManager.TriggerGameOver();
		}
	}

	public void LoseHealth()
	{
		health--;
		UpdateHealthUI();
	}

	public bool IsOutOfLives() => health <= 0;

    void UpdateScoreUI()
    {
        scoreText.text = score.ToString("D6");
    }

	void  UpdateHealthUI()
	{
		for (int i = 0; i < healthBar.Length; i++)
		{	
			healthBar[i].SetActive(i < health);
		}
	}

	public int GetScore() => score;

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
		scaredActive = true;
		recoveringActive = false;

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
				recoveringActive = true;
            }

            yield return null;
        }

        SetGhostsState(0); 
        musicScript.goblinIsScared = false;
        scaredTimerDisplay.scaredTimeRemaining = 0;
        scaredCoroutine = null;
		scaredActive = false;
		recoveringActive = false;
    }

    void SetGhostsState(int state)
    {
	    foreach (GhostCollision ghost in ghosts)
	    {
		    if (ghost.ghostState == 3) continue;
		    ghost.SetState(state);
	    }
    }

	void Start()
	{
    	
	}

	int CountTotalPellets()
	{
    	int count = 0;
    	for (int r = 0; r < LevelGenerator.Rows; r++)
    	{
        	for (int c = 0; c < LevelGenerator.Cols; c++)
        {

        int tile = LevelGenerator.levelMap[r, c];
        if (tile == 5 || tile == 6) count++;
        }
    }
    return count;
	}

	public void HandlePacStudentDeath()
	{
    StartCoroutine(PacStudentDeathSequence());
	}

	IEnumerator PacStudentDeathSequence()
	{
    	pacStudent.canMove = false;
    	deathParticle.Play();

    	pacStudent.Die();
    	LoseHealth();

		yield return new WaitForSeconds(2f);

		deathParticle.Stop();

		if (IsOutOfLives())
    	{
        	gameOverManager.TriggerGameOver();
    	}
    	else
    	{
        	pacStudent.Respawn();
		}
	}

	public void HandleGhostEaten(GhostCollision ghost)
	{
		AddScore(300);
		soundEffects.PlayCleanGoblin();
		musicScript.goblinIsDead = true;

		ghost.SetState(3);

		StartCoroutine(GhostRespawnTimer(ghost));
	}	

	IEnumerator GhostRespawnTimer(GhostCollision ghost)
	{	
		
    	yield return new WaitForSeconds(3f);
	    
    	int targetState;
		if (!scaredActive)
		{	
	 		targetState = 0;
		}	
		else if (recoveringActive)
		{
			targetState = 2;
		}
		else
		{
			targetState = 1;
		}

    	ghost.SetState(targetState);

	    bool anyGhostStillDead = System.Array.Exists(ghosts, g => g.ghostState == 3);
   		if (!anyGhostStillDead)
    		{
        	musicScript.goblinIsDead = false;
    		}
	}	
}