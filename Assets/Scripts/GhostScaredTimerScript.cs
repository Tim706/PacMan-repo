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
            scaredTimer.enabled = false;
        }
        else
        {
            scaredTimer.enabled = true;
            scaredTimer.text = scaredTimeRemaining.ToString();
        }
        
    }
}
