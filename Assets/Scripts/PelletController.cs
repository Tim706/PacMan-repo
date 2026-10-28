using UnityEngine;

public class PelletController : MonoBehaviour
{

    public int scoreValue = 10;

    void OnTriggerEnter2D(Collider2D entity)
    {
        if (!entity.CompareTag("Player"))
        {
            return;
        }
         
        GameManager.Instance.AddScore(scoreValue);
        Destroy(gameObject);
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
