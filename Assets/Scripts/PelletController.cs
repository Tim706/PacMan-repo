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

        EatPellet();
    }
    
    void Start()
    {
        Vector2Int gridPos = LevelGenerator.WorldToGrid(transform.position);
        row = gridPos.x;
        col = gridPos.y;

		Collider2D myCollider = GetComponent<Collider2D>();
        Collider2D[] overlaps = Physics2D.OverlapBoxAll(transform.position, myCollider.bounds.size, 0f);
        foreach (var overlap in overlaps)
        {
            if (overlap.CompareTag("Player"))
            {
                EatPellet();
                return;
            }
        }
    }

	void EatPellet()
    {
        GameManager.Instance.AddScore(scoreValue);
        LevelGenerator.MarkPelletEaten(row, col);
        GameManager.Instance.PelletsEatenTracker();
        Destroy(gameObject);
    }
    
    void Update()
    {
        
    }
}
