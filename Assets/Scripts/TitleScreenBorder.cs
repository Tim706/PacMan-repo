using UnityEngine;

public class TitleScreenBorder : MonoBehaviour
{
    
    public Animator animator;
    public RectTransform border;
    public float speed = 0.2f;
    public float startpos = 0f;
    public float progress;
    
    void Start()
    {
        progress = startpos;
    }

    
    void Update()
    {
        progress += speed * Time.deltaTime;
        if (progress > 1f) progress -= 1f;

        Vector2 pos = GetPerimeterPosition(progress);
        transform.localPosition = pos;
    }

    Vector2 GetPerimeterPosition(float p)
    {
        float width = border.rect.width;
        float height = border.rect.height;
        float perimter = 2 * (width + height);
        float distance = p * perimter;

        if (distance < width)
        {
            animator.SetInteger("Direction", 0);
            return new Vector2(-width / 2 + distance, height / 2);
        }
        distance -= width;
        
        if (distance < height)
        {
            animator.SetInteger("Direction", 1);
            return new Vector2(width / 2, height / 2 - distance);
        }
        distance -= height;
        
        if (distance < width)
        {
            animator.SetInteger("Direction", 2);
            return new Vector2(width / 2 - distance, -height / 2);
        }
        distance -= width;
        
        animator.SetInteger("Direction", 3);
        return new Vector2(-width/2, -height/2 + distance);
    }
}
