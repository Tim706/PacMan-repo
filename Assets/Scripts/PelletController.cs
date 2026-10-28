using UnityEngine;

public class PelletController : MonoBehaviour
{

    public int scoreValue = 10;
    
    private int row;
    private int col;

    void OnTriggerEnter2D(Collider2D entity)
    {
        if (!entity.CompareTag("Player"))
        {
            return;
        }
         
        GameManager.Instance.AddScore(scoreValue);
        LevelGenerator.MarkPelletEaten(row, col);
        Destroy(gameObject);
    }
    
    void Start()
    {
        Vector2Int gridPos = LevelGenerator.WorldToGrid(transform.position);
        row = gridPos.x;
        col = gridPos.y;
    }
    
    void Update()
    {
        
    }
}
