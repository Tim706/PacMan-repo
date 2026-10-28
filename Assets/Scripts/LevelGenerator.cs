using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private static readonly int[,] quadrant =
    {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,8},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    };

    public static int[,] levelMap { get; private set; }

    public static int Rows => levelMap.GetLength(0);
    public static int Cols => levelMap.GetLength(1);

	public const float  OffsetX = -28.0573f;
	public const float  OffsetY = 25.0f;

	public static float MapLeft => OffsetX;
	public static float MapRight => OffsetX + (Cols - 1);
	public static float MapTop => OffsetY;
	public static float MapBot => OffsetY - (Rows - 1);

	public static Vector3 GridToWorld(int row, int col) 
	{
		float worldX = OffsetX + col;
 		float worldY = OffsetY - row;
		return new Vector3(worldX, worldY, 0f);
	}

    void Awake()
    {
        BuildFullMap();
    }

    private static void BuildFullMap()
    {
        int qRows = quadrant.GetLength(0);
        int qCols = quadrant.GetLength(1);

        int fullRows = qRows * 2;
        int fullCols = qCols * 2;

        levelMap = new int[fullRows, fullCols];

        for (int r = 0; r < qRows; r++)
        {
            for (int c = 0; c < qCols; c++)
            {
                int value = quadrant[r, c];
                
                levelMap[r, c] = value;
                
                levelMap[r, fullCols - 1 - c] = value;
                
                levelMap[fullRows - 1 - r, c] = value;
                
                levelMap[fullRows - 1 - r, fullCols - 1 - c] = value;
            }
        }
    }
    
	private static int GetTile(int row, int col)
	{
		 if (row < 0 || row >= Rows || col < 0 || col >= Cols)
            return -1;

        return levelMap[row, col];
	}

	private static bool IsWallType(int tile) =>
		tile == 1 || tile == 2 || tile == 3 || tile == 4 || tile == 7;

    public static bool IsWalkablePlayer(int row, int col)
    {
        int tile = GetTile(row, col);
		if  (tile == -1) return false;
        return !IsWallType(tile) && tile != 8;
    }
    
    public static bool IsWalkableGhost(int row, int col, bool comingFromInside)
    {
        int tile = GetTile(row, col);
		if  (tile == -1) return false;
        if (tile == 8) return comingFromInside;
		return !IsWallType(tile);
    }
}
