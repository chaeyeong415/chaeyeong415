using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Dreamplayer : MonoBehaviour
{
    public float moveSpeed = 4f;
    [Min(0.01f)] public float frameTime = 0.15f;

    public Sprite[] downSprites = new Sprite[3];
    public Sprite[] upSprites = new Sprite[3];
    public Sprite[] leftSprites = new Sprite[3];
    public Sprite[] rightSprites = new Sprite[3];

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 moveInput;
    private Vector2 facingDir = Vector2.down;
    private int frameIndex;
    private float frameTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        if (moveInput != Vector2.zero)
        {
            facingDir = moveInput;
        }
    }

    private void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;

        Sprite[] frames = GetFacingSprites();
        if (frames == null || frames.Length == 0) return;

        if (moveInput == Vector2.zero)
        {
            frameIndex = 0;
            frameTimer = 0f;
        }
        else
        {
            frameTimer -= Time.deltaTime;
            if (frameTimer <= 0f)
            {
                frameIndex++;
                frameTimer = frameTime;
            }
        }

        frameIndex %= frames.Length;
        Sprite frame = frames[frameIndex];
        if (frame != null) sr.sprite = frame;
    }

    private Sprite[] GetFacingSprites()
    {
        if (facingDir.y > 0f) return upSprites;
        if (facingDir.y < 0f) return downSprites;
        if (facingDir.x < 0f) return leftSprites;
        return rightSprites;
    }
}