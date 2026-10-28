using UnityEngine;

public class CherryCollision : MonoBehaviour
{
    public int scoreValue = 100;
    public SoundEffectsScript soundEffects;

    void OnTriggerEnter2D(Collider2D entity)
    {
        if (!entity.CompareTag("Player"))
            return;

        GameManager.Instance.AddScore(scoreValue);
        soundEffects.PlayGainWater();
        Destroy(gameObject);
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
