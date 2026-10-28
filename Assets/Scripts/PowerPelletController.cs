using UnityEngine;

public class PowerPillController : MonoBehaviour
{
    private int row;
    private int col;
    void Start()
    {
        Vector2Int gridPos = LevelGenerator.WorldToGrid(transform.position);
        row = gridPos.x;
        col = gridPos.y;
    }
    
    void OnTriggerEnter2D(Collider2D entity)
    {
        if (!entity.CompareTag("Player"))
            return;

        LevelGenerator.MarkPelletEaten(row, col);
        GameManager.Instance.ActivatePowerPill();
        Destroy(gameObject);
    }
    
    void Update()
    {
        
    }
}
