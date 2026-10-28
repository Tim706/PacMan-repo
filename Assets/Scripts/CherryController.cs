using UnityEngine;

public class CherryController : MonoBehaviour
{

    [Header("Cherry Object")] 
    public GameObject cherryPrefab;

    [Header("Timing and Movement")] 
    public float respawnDelay = 5f;
    public float moveSpeed = 4f;
    public float spawnMargin = 1.5f;

    private float timer = 0f;
    private GameObject currentCherry;

    private Vector3 startPos;
    private Vector3 endPos;
    private float elapsedTime;
    private float totalDistance;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (currentCherry != null)
        {
            MoveCherry();
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= respawnDelay)
            {
                SpawnCherry();
                timer = 0f;
            }
        }
    }

    void SpawnCherry()
    {
        float mapLeft = LevelGenerator.MapLeft;
        float mapRight = LevelGenerator.MapRight;
        float mapTop = LevelGenerator.MapTop;
        float mapBottom = LevelGenerator.MapBot;

        float centerX = (mapLeft + mapRight) / 2f;
        float centerY = (mapTop + mapBottom) / 2f;

        int edge = Random.Range(0, 4);

        switch (edge)
        {
            case 0:
                startPos = new Vector3(mapLeft - spawnMargin, Random.Range(mapBottom, mapTop), 0);
                break;
            case 1:
                startPos = new Vector3(mapRight + spawnMargin, Random.Range(mapBottom, mapTop), 0);
                break;
            case 2:
                startPos = new Vector3(Random.Range(mapLeft, mapRight), mapTop + spawnMargin, 0);
                break;
            default:
                startPos = new Vector3(Random.Range(mapLeft, mapRight), mapBottom - spawnMargin, 0);
                break;
        }
        
        Vector3 center = new Vector3(centerX, centerY, 0);
        endPos = center + (center - startPos);

        currentCherry = Instantiate(cherryPrefab, startPos, Quaternion.identity);

        elapsedTime = 0f;
        totalDistance = Vector3.Distance(startPos, endPos);
    }

    void MoveCherry()
    {
        elapsedTime += Time.deltaTime;
        float t = (totalDistance > 0f) ? (elapsedTime * moveSpeed) / totalDistance : 1f;

        if (t >= 1f)
        {
            Destroy(currentCherry);
            currentCherry = null;
            return;
        }
        
        currentCherry.transform.position = Vector3.Lerp(startPos, endPos, t);
    }
}
