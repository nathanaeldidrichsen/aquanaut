using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [Header("Movement")]
    public float normalMoveSpeed = 5f; // Normal movement speed
    public float boostedMoveSpeed = 7f; // Movement speed when Shift is held down
    private float moveSpeed;
    private Vector3 mousePosition;
    public float jumpForce = 5f; // Jump force
    private float gfxAngle;
    public float rotationSpeed;
    private Vector2 moveDirection;
    public int facingDir = 1; //1 = facing right -1 facing left


    [Header("Other")]
    private Shooting shooting;
    public Health health;
    public Oxygen oxygen;
    [SerializeField] public GameObject gfx;
    private bool isKnockedBack;
    private float normalOxygenDepletionRate;


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
        normalOxygenDepletionRate = oxygen.oxygenDepletionRate;
        
    }

    void FixedUpdate()
    {
        if(!isKnockedBack)
        {
        rb.velocity = moveDirection * Time.fixedDeltaTime;
        }
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
            oxygen.oxygenDepletionRate = (int)normalOxygenDepletionRate * 2;
        }
        else
        {
            oxygen.oxygenDepletionRate = (int)normalOxygenDepletionRate;
        }


        moveDirection = new Vector2(horizontalInput, verticalInput) * moveSpeed;

        RotateGFX();


        if (facingDir == -1)
        {
            gfx.GetComponent<SpriteRenderer>().flipX = true;

        }
        else
        {
            gfx.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    void RotateGFX()
    {

        // Flip GameObject's X localScale based on movement direction
        if (moveDirection.x < 0) // Moving left
        {
            gfx.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            facingDir = -1;

            //If theres no z rotation when turning left, double the movedirX
            if (gfx.transform.rotation.z == 0)
            {
                gfxAngle = Mathf.Atan2(Mathf.Abs(moveDirection.y), Mathf.Abs(moveDirection.x) * 2) * Mathf.Rad2Deg;

            }
            else
            {
                gfxAngle = Mathf.Atan2(Mathf.Abs(moveDirection.y), -Mathf.Abs(moveDirection.x) * 2) * Mathf.Rad2Deg;
            }

            if (gfxAngle != 0 && moveDirection.y > 0)
            {
                gfxAngle = -gfxAngle;
            }
        }
        else if (moveDirection.x > 0) // Moving right
        {
            facingDir = 1;

            // gfx.transform.localScale = new Vector3(1f, 1f, 1f); 
            gfxAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        }

        // if(rb.velocity.y == 0)
        // {
        //     gfxAngle = 0;
        // }
        gfx.transform.rotation = Quaternion.Euler(0f, 0f, gfxAngle);
    }

        public IEnumerator ApplyKnockBack()
    {
        isKnockedBack = true;
        yield return new WaitForSeconds(0.5f);
        isKnockedBack = false;

    }

}
