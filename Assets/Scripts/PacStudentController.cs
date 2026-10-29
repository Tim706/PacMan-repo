using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    
    [Header("Movement Settings")]
    public float moveSpeed = 5.0f;
    
    [Header("Animation/SFX")]
    public Animator animator;
    public SoundEffectsScript soundEffects;
    
    [Header("DustParticle")]
    public ParticleSystem dustEffect;
    
    private int currentRow;
    private int currentCol;
    private int targetRow;
    private int targetCol;
    private bool isLerping = false;
    private float lerpProgress = 0.0f;
    
    private enum Direction { Right, Left, Up, Down }
    private Direction lastInput = Direction.Right;
    private Direction currentInput = Direction.Right;
    public bool canMove = false;
    
    private bool hasHitWall = false;
    private int tunnelLockRemaining = 0;
    private Direction tunnelLockDirection;
    private bool tunnelLocked = false;
    
    void Start()
    {
        currentRow = 1;
        currentCol = 1;
        
        transform.position = LevelGenerator.GridToWorld(currentRow, currentCol);
        
        lastInput = Direction.Right;
        currentInput = Direction.Right;
        SetAnimatorDirection(Direction.Right);
        //StopMovementFeedback();

    }
    
    void Update()
    {

        if (!canMove)
        {
            return;
        }
        
        ScanInput();

        if (!isLerping)
        {
            TryMove();
        }
        else
        {
            ContinueLerp();
        }
        
        if (Input.GetKeyDown(KeyCode.L)) // L for "Lose life", temporary test key
        {
            GameManager.Instance.HandlePacStudentDeath();
        }
    }

    void ScanInput()
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
            hasHitWall = false;
            return;
        }
        
        if (TryMoveDirection(currentInput))
        {
            hasHitWall = false;
            return;
        }

        if (!hasHitWall && !tunnelLocked)
        {
            soundEffects.SFXSource.Stop();
            soundEffects.PlayWallHit();
            hasHitWall = true;
            animator.speed = 0f;
            dustEffect.Stop();
        }
    }

    bool TryMoveDirection(Direction dir)
    {

        tunnelLocked = false;
        if (tunnelLockRemaining > 0 && dir == OppositeDirection(tunnelLockDirection))
        {
            tunnelLocked = true;
            return false;
        }
        
        int newRow = currentRow;
        int newCol = currentCol;

        switch (dir)
        {
            case Direction.Up:    newRow -= 1; break;
            case Direction.Down:  newRow += 1; break;
            case Direction.Left:  newCol -= 1; break;
            case Direction.Right: newCol += 1; break;
        }

        bool isTunnelRow = (currentRow == 14 || currentRow == 15);

        if (isTunnelRow && dir == Direction.Left && newCol < 0)
        {
            currentCol = LevelGenerator.Cols;
            newCol = currentCol - 1;
            tunnelLockRemaining = 4;
            tunnelLockDirection = Direction.Left;
        }
        
        else if (isTunnelRow && dir == Direction.Right && newCol >= LevelGenerator.Cols)
        {
            currentCol = -1;
            newCol = currentCol + 1;
            tunnelLockRemaining = 4;
            tunnelLockDirection = Direction.Right;
        }

        if (!LevelGenerator.IsWalkablePlayer(newRow, newCol))
        {
            return false;
        }
            

        if (!dustEffect.isPlaying)
        {
            dustEffect.Play();
        }
        
        targetRow = newRow;
        targetCol = newCol;
        isLerping = true;
        lerpProgress = 0.0f;

        if (tunnelLockRemaining > 0)
        {
            tunnelLockRemaining--;
        }
        
        animator.speed = 1f;
        SetAnimatorDirection(dir);
        StartMovementFeedback(newRow, newCol);

        return true;
    }
    
    private Direction OppositeDirection(Direction dir)
    {
        return dir switch
        {
            Direction.Up => Direction.Down,
            Direction.Down => Direction.Up,
            Direction.Left => Direction.Right,
            Direction.Right => Direction.Left,
            _ => dir
        };
    }

    void ContinueLerp()
    {
        lerpProgress += moveSpeed * Time.deltaTime;

        Vector3 startPos = LevelGenerator.GridToWorld(currentRow, currentCol);
        Vector3 endPos = LevelGenerator.GridToWorld(targetRow, targetCol);

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
        bool aboutToEatPellet = LevelGenerator.HasPelletAt(nextRow, nextCol);

        if (aboutToEatPellet)
        {
            soundEffects.PlayCleaning();
        }

        else
        {
            soundEffects.PlayMoving();
        }
    }
    
    public void Die()
    {
        canMove = false;
        soundEffects.SFXSource.Stop();
        dustEffect.Stop();
        animator.speed = 1f; 
        animator.SetBool("IsDead", true);
        soundEffects.PlayDeath();
    }

    public void Respawn()
    {
        animator.SetBool("IsDead", false);

        currentRow = 1;
        currentCol = 1;
        transform.position = LevelGenerator.GridToWorld(currentRow, currentCol);

        lastInput = Direction.Right;
        currentInput = Direction.Right;
        SetAnimatorDirection(Direction.Right);

        isLerping = false;
        lerpProgress = 0f;
        hasHitWall = false;
        tunnelLockRemaining = 0;

        canMove = true;
    }

    //void StopMovementFeedback()
    //{
        //soundEffects.SFXSource.Stop();
        //animator.speed = 0f;
        //dustEffect.Stop();
    //}
}
