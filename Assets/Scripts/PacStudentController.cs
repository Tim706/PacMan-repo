using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    
    [Header("Movement Settings")]
    public float moveSpeed = 5.0f;
    
    [Header("Animation/SFX")]
    public Animtor animtor;
    public SoundEffectsScript soundEffects;

    //top left most tile placed at these coords, not 0,0
    public float offsetX = -28.0573f;
    public float offsetY = 25f;
    
    private int currentRow;
    private int currentCol;
    private int targetRow;
    private int targetCol;
    private bool isLerping = false;
    private float lerpProgress = 0.0f;
    
    private enum Direction { Right, Left, Up, Down }
    private Direction lastInput = Direction.Right;
    private Direction currentInput = Direction.Right;
    
    void Start()
    {
        currentRow = 24;
        currentCol = 14;
        
        transform.position = GridToWorld(currentRow, currentCol);
        
        lastInput = Direction.Right;
        currentInput = Direction.Right;
        SetAnimatorDirection(Direction.Right);
        StopMovementFeedback();

    }
    
    void Update()
    {
        Input();

        if (!isLerping)
        {
            TryMove();
        }
        else
        {
            ContinueLerp();
        }
    }

    void Input()
    {
        if (Input.GetKeyDown(KeyCode.W)) lastInput = Direction.Up;
        else if (Input.GetKeyDown(KeyCode.A)) lastInput = Direction.Left;
        else if (Input.GetKeyDown(KeyCode.S)) lastInput = Direction.Down;
        else if (Input.GetKeyDown(KeyCode.D)) lastInput = Direction.Right;
    }

    void TryMove()
    {
        if (TryMoveDirection(lastInput))
        {
            currentInput = lastInput;
            return;
        }
        
        if (TryMoveDirection(currentInput))
        {
            return;
        }
        
        StopMovementFeedback();
    }

    bool TryMoveDirection(Direction dir)
    {
        int newRow = currentRow;
        int newCol = currentCol;

        switch (dir)
        {
            case Direction.Up:    newRow -= 1; break;
            case Direction.Down:  newRow += 1; break;
            case Direction.Left:  newCol -= 1; break;
            case Direction.Right: newCol += 1; break;
        }

        if (!LevelGenerator.IsWalkable(newRow, newCol))
            return false;
        
        targetRow = newRow;
        targetCol = newCol;
        isLerping = true;
        lerpProgress = 0.0f;

        SetAnimatorDirection(dir);
        StartMovementFeedback(newRow, newCol);

        return true;
    }

    void ContinueLerp()
    {
        lerpProgress += moveSpeed * Time.deltaTime;

        Vector3 startPos = GridToWorld(currentRow, currentCol);
        Vector3 endPos = GridToWorld(targetRow, targetCol);

        if (lerpProgress >= 1f)
        {
            transform.position = endPos;
            currentRow = targetRow;
            currentCol = targetCol;
            isLerping = false;
            lerpProgress = 0f;
        }
        else
        {
            transform.position = Vector3.Lerp(startPos, endPos, lerpProgress);
        }
    }

    Vector3 GridToWorld(int row, int col)
    {
        float worldX = offsetX + col;
        float worldY = offsetY - row;
        return new Vector3(worldX, worldY, 0);
    }

    void SetAnimatorDirection(Direction dir)
    {
        int directionValue = dir switch
        {
            Direction.Right => 0,
            Direction.Down => 1,
            Direction.Left => 2,
            Direction.Up => 3,
            _ => 0
        };
        animator.SetInteger("Direction", directionValue);
    }

    void StartMovementFeedback(int nextRow, int nextCol)
    {
        int tile = LevelGenerator.levelMap[nextRow, nextCol];
        bool aboutToEatPellet = (tile == 5 || tile == 6);

        if (aboutToEatPellet)
            soundEffects.PlayEatPellet();
        else
            soundEffects.PlayMoving();

        // TODO: enable dust particle effect here
    }

    void StopMovementFeedback()
    {
        // TODO: stop movement audio/dust particle effect when idle
    }
}
