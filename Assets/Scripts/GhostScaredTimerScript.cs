using UnityEngine;
using TMPro;

public class GhostScaredTimerScript : MonoBehaviour
{

    public TextMeshProUGUI scaredTimer;
    public int scaredTimeRemaining;
    
    
    void Start()
    {
        
    }

   
    void Update()
    {
        if (scaredTimeRemaining <= 0)
        {
            scaredTimer.gameObject.SetActive(false);
        }
        else
        {
            scaredTimer.gameObject.SetActive(true);
            scaredTimer.text = scaredTimeRemaining.ToString();
        }
        
    }
}
