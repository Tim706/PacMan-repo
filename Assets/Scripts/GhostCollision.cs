using UnityEngine;

public class GhostCollision : MonoBehaviour
{
    
    public int ghostState = 0; 
	public Animator ghostAnimator;

    public void SetState(int state)
    {
        ghostState = state;
        ghostAnimator.SetInteger("Ghost State", state);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (ghostState == 0)
        {
            GameManager.Instance.HandlePacStudentDeath();
        }
        else if (ghostState == 1 || ghostState == 2)
        {
            GameManager.Instance.HandleGhostEaten(this);
        }
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}