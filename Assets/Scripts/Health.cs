using System.Collections;
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
    [SerializeField] private GameObject healthSliderObject;
    public int deadCoinAmount = 10;

    



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth; // Initialize current health to maximum health
        recoveryCounter =  GetComponent<RecoveryCounter>();
    }

public void GetHurt(int damageAmount, Vector2 attackedFromPosition)
{

    if(this.gameObject.CompareTag("Nest"))
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        } 
    }

    if (!recoveryCounter.recovering)
    {
        recoveryCounter.Recover();
        Vector2 attackDirection = (transform.position - (Vector3)attackedFromPosition).normalized;
        if(creature != null)
        {
        SoundManager.Instance.PlaySound(SoundManager.Instance.enemyHitSound, 0.5f);
            creature.wasAttacked = true;
            StartCoroutine(ShowHealthAfterHit());
            StartCoroutine(creature.ApplyKnockBack());
        }
        
        if(isPlayer)
        {
        SoundManager.Instance.PlaySound(SoundManager.Instance.biteSound, 0.5f);
            
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

        if (isPlayer)
        {
            HUD.Instance.LostGame();
        }


        HUD.Instance.GetMoney(deadCoinAmount);
        Destroy(gameObject);
    }

    public IEnumerator ShowHealthAfterHit()
    {
            if(healthSliderObject != null)
            {
            healthSliderObject.SetActive(true);
            yield return new WaitForSeconds(5);
            healthSliderObject.SetActive(false);
            }
    }

}
