using UnityEngine;
using TMPro;
using System.Collections;

public class IntroTimer : MonoBehaviour
{

    [Header("IntroUI")]
    public GameObject screenBlocker;
    public TextMeshProUGUI countDownText;

    [Header("Connectors")] 
    public PacStudentController pacStudent;
    public MusicScript musicScript;
    public GameTimerScript gameTimer;
    public CherryController cherryController;
    
    void Start()
    {
        StartCoroutine(RoundStartSequence());
    }
    
    IEnumerator RoundStartSequence()
    {
        pacStudent.canMove = false;

        screenBlocker.SetActive(true);
        countDownText.gameObject.SetActive(true);

        musicScript.inGame = true;
        musicScript.playingIntro = true;

        string[] steps = { "3", "2", "1", "GO!" };
        foreach (string step in steps)
        {
            countDownText.text = step;
            yield return new WaitForSeconds(1f);
        }

        screenBlocker.SetActive(false);
        countDownText.gameObject.SetActive(false);
        
        musicScript.playingIntro = false;
        musicScript.goblinIsScared = false;
        musicScript.goblinIsDead = false;

        pacStudent.canMove = true;
        gameTimer.StartTimer();
        cherryController.canSpawn = true;
    }
    
    void Update()
    {
        
    }
}
