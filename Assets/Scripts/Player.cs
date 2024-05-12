using UnityEngine;

public class Player : MonoBehaviour
{
    public float normalMoveSpeed = 5f; // Normal movement speed
    public float boostedMoveSpeed = 7f; // Movement speed when Shift is held down
    private float moveSpeed;
    private Vector3 mousePosition;
    public float jumpForce = 5f; // Jump force
    public Health health;
    public Oxygen oxygen;
    private Vector2 moveDirection;
    [SerializeField] GameObject gfx;
    private float gfxAngle;



    private Rigidbody2D rb;
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

        // mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Vector3 rotation = mousePosition - harpoonShootPoint.transform.position;
        // float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        // harpoonShootPoint.transform.rotation = Quaternion.Euler(0,0,rotZ);

        // Handle shooting
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            // harpoonShootPoint.SetActive(true);
            // ShootHarpoon();
        }


        // Flip GameObject's X localScale based on movement direction
        if (moveDirection.x < 0) // Moving left
        {
            gfx.transform.localScale = new Vector3(-1f, 1f, 1f); // Flip X localScale
        }
        else if (moveDirection.x > 0) // Moving right
        {
            gfx.transform.localScale = new Vector3(1f, 1f, 1f); // Reset X localScale

        }

    // // Rotate gfx towards moveDirection with limited angle
    // if (moveDirection.magnitude > 0) // Check if there is movement
    // {
    //     float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
    //     angle = Mathf.Clamp(angle, -60f, 60f); // Limit angle to ±60 degrees
    //     gfx.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    // }
    }
}
