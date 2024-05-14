using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float normalMoveSpeed = 5f; // Normal movement speed
    public float boostedMoveSpeed = 7f; // Movement speed when Shift is held down
    private float moveSpeed;
    private Vector3 mousePosition;
    public float jumpForce = 5f; // Jump force
    private Shooting shooting;
    public Health health;
    public Oxygen oxygen;
    private Vector2 moveDirection;
    [SerializeField] GameObject gfx;
    private float gfxAngle;
    public float rotationSpeed;


    public Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<Player>();
            return instance;
        }
    }

    private void Start()
    {
        shooting = GetComponent<Shooting>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        health = GetComponent<Health>();
        oxygen = GetComponentInChildren<Oxygen>();
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection * Time.fixedDeltaTime;
    }
    private void Update()
    {
        // Horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float moveSpeed = normalMoveSpeed; // Default to normal move speed

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            moveSpeed = boostedMoveSpeed; // If Shift is held down, use boosted move speed
        }

        moveDirection = new Vector2(horizontalInput, verticalInput) * moveSpeed;

        // RotateGFX();
        // if (moveDirection.magnitude > 0) // Check if there is movement
        // {
            RotateGFX();
            // RotateGfxTowardsMoveDirection();
        // }
    }

    void RotateGFX()
    {
        
        // Flip GameObject's X localScale based on movement direction
        if (moveDirection.x < 0) // Moving left
        {
            Debug.Log("Left");
            gfx.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            gfx.GetComponent<SpriteRenderer>().flipX = true;
            
            if(gfx.transform.rotation.z == 0)
            {
            gfxAngle = Mathf.Atan2(Mathf.Abs(moveDirection.y), Mathf.Abs(moveDirection.x) *2) * Mathf.Rad2Deg;
            }
            else
            {
            gfxAngle = Mathf.Atan2(Mathf.Abs(moveDirection.y), -Mathf.Abs(moveDirection.x) *2) * Mathf.Rad2Deg;
            }

            if(gfxAngle != 0 && moveDirection.y > 0)
            {
            gfxAngle = -gfxAngle;

            }

            gfx.transform.rotation = Quaternion.Euler(0f, 0f, gfxAngle);
        }
        else if (moveDirection.x > 0) // Moving right
        {
            Debug.Log("Right");
            gfx.GetComponent<SpriteRenderer>().flipX = false;
            // gfx.transform.localScale = new Vector3(1f, 1f, 1f); 
            gfxAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            gfx.transform.rotation = Quaternion.Euler(0f, 0f, gfxAngle);
        }
    }

        private void RotateGfxTowardsMoveDirection()
    {
        // Calculate the angle in degrees
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

        // Determine if the character is moving left or right
        bool movingLeft = moveDirection.x < 0;

        if (movingLeft ) // Moving left
        {
            gfx.transform.localScale = new Vector3(-1f, 1f, 1f); // Flip X localScale
            gfx.transform.rotation = Quaternion.Euler(0f, 0f, -gfxAngle); // Apply the opposite angle
        }
        else // Moving right
        {
            gfx.transform.localScale = new Vector3(1f, 1f, 1f); // Reset X localScale
            gfx.transform.rotation = Quaternion.Euler(0f, 0f, gfxAngle); // Apply the angle
        }
    }
}
