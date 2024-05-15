using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{

    public bool isCapturable;
    public int captureTime;
    private Rigidbody2D rb;
    private ProcessCollection process;
    public float speed = 5f;
    public int damage = 3;
    public bool isHooked = false;
    private Health health;
    [SerializeField] private float currentSpeed;
    public bool isAggressive;
    public bool wasAttacked = false;
    private Vector2 moveDirection;
    [SerializeField] public GameObject gfx;
    private float gfxAngle;
    public float rotationSpeed;
    private bool isKnockedBack;
    [SerializeField] private float rbVelocityX;
    public int facingDir = 1; //1 = facing right -1 facing left


    void Start()
    {
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        process = GetComponent<ProcessCollection>();
    }

    // Update is called once per frame
    void Update()
    {
        rbVelocityX = rb.velocity.x * speed;

        // UpdateSpeedBasedOnHealth();
        if (isHooked || wasAttacked)
        {
            Vector2 directionToPlayer = Player.Instance.transform.position - transform.position;
            directionToPlayer.Normalize(); // Ensure the direction vector is of length 1

            if (isAggressive)
            {
                // Move towards Player
                if (!isKnockedBack)
                {
                    rb.velocity = directionToPlayer * speed;
                }
            }
            else
            {
                if (!isKnockedBack)
                {
                    rb.velocity = -directionToPlayer * speed;
                }
            }
        }

        if(!isKnockedBack)
        {
        moveDirection = new Vector2(rb.velocity.x, rb.velocity.y) * speed;
        }

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

    private void UpdateSpeedBasedOnHealth()
    {
        currentSpeed = speed * (health.currentHealth / health.maxHealth);
        // rb.velocity = rb.velocity.normalized * currentSpeed; // Apply the new speed
    }

    public void StartCapture()
    {
        if (isCapturable)
        {
            process.StartTimer(captureTime);
        }
    }

    public void Captured()
    {
        Player.Instance.GetComponentInChildren<Shooting>().UnhookCreature();
        Destroy(gameObject);
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
            gfxAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        }

        gfx.transform.rotation = Quaternion.Euler(0f, 0f, gfxAngle);
    }

    void OnCollisionStay2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Health>().GetHurt(damage, transform.position);
        }
    }

    public IEnumerator ApplyKnockBack()
    {
        isKnockedBack = true;
        yield return new WaitForSeconds(0.2f);
        isKnockedBack = false;
    }
}
