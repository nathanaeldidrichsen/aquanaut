using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health
    public int currentHealth; // Current health¨
    public bool isPlayer;
    public float knockBackForce = 30f;
    private Rigidbody2D rb;
    private RecoveryCounter recoveryCounter;
    [SerializeField] private Creature creature;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth; // Initialize current health to maximum health
        recoveryCounter =  GetComponent<RecoveryCounter>();
    }

public void GetHurt(int damageAmount, Vector2 attackedFromPosition)
{
    if (!recoveryCounter.recovering)
    {
        recoveryCounter.Recover();
        Vector2 attackDirection = (transform.position - (Vector3)attackedFromPosition).normalized;
        if(creature != null)
        {
            StartCoroutine(creature.ApplyKnockBack());
        }
        else
        {
            StartCoroutine(Player.Instance.ApplyKnockBack());
        }
        rb?.AddForce(attackDirection * knockBackForce, ForceMode2D.Impulse);
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
}


    private void Die()
    {
        Destroy(gameObject);
        if(isPlayer)
        {
        HUD.Instance.LostGame();
        }
    }
}
