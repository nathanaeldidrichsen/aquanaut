using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


/*Manages and updates the HUD, which contains your health bar, coins, etc*/

public class HUD : MonoBehaviour
{
    [Header("Reference")]
    // public Animator anim;
    //[SerializeField] private GameObject healthBar;
    //[SerializeField] private GameObject startUp;
    [SerializeField] private GameObject deadScreen;
    [SerializeField] private GameObject pauseScreen;
    private bool isPaused = false;


    public TextMeshProUGUI coinsText;
    public int coinAmount;
    private Animator anim;
    public TextMeshProUGUI collectedCoinsText;



    public TextMeshProUGUI uiHealth;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider oxygenSlider;

    [System.NonSerialized] public string loadSceneName;
    [System.NonSerialized] public bool resetPlayer;
    public Animator playerStatusAnim;
    private bool inventoryIsOpen;

    private static HUD instance;
    public static HUD Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<HUD>();
            return instance;
        }
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        coinsText.text = coinAmount.ToString();
        Time.timeScale = 1;
        deadScreen.SetActive(false);
        //Set all bar widths to 1, and also the smooth variables.
        //healthBarWidth = 1;
        //healthBarWidthEased = healthBarWidth;
    }

    void Update()
    {


        if(healthSlider != null & Player.Instance != null)
        {
            healthSlider.value = Player.Instance.health.currentHealth;
            healthSlider.maxValue = Player.Instance.health.maxHealth;
        }

        if (oxygenSlider != null & Player.Instance != null)
        {
            oxygenSlider.value = Player.Instance.oxygen.currentOxygenLevel;
            oxygenSlider.maxValue = Player.Instance.oxygen.maxOxygenLevel;
        }


        //Controls the width of the health bar based on the player's total health
        //healthBarWidth = (float)Player.Instance.health / (float)Player.Instance.maxHealth;
        // healthBarWidthEased += (healthBarWidth - healthBarWidthEased) * Time.deltaTime * healthBarWidthEased;
        // healthBar.transform.localScale = new Vector2(healthBarWidthEased, 1);
    }

    public void GetMoney(int amount)
    {
        coinAmount += amount;
        coinsText.text = coinAmount.ToString();
        collectedCoinsText.text = "$"+amount.ToString();
        playerStatusAnim.SetTrigger("earn");
    }

    public void PlayCoverScreenAnimation()
    {
        anim.CrossFadeInFixedTime("hud_startscene", 0.01f);
    }

/*    public void OpenInventory()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.npcSound, 0.1f);
        if (inventoryScreen.activeSelf)
        {

            Time.timeScale = 1;
            if(Inventory.Instance.isHoldingItem)
            {
                Inventory.Instance.DropItem();
            }
            inventoryScreen.SetActive(false);
        }
        else
        {
            inventoryScreen.SetActive(true);
            Inventory.Instance.SelectFirstSlot();
            Time.timeScale = 0;
        }
    }*/

    public void HealthBarHurt()
    {
        //animator.SetTrigger("hurt");
    }

    public void LostGame()
    {
        deadScreen.SetActive(true);
        Time.timeScale = 0;
    }

/*    public void IncreaseDamage()
    {
        // Player.Instance.stats.damage = (int)(Player.Instance.stats.damage * 1.1f); // Increase damage by 10%
        // int increaseAmount = Mathf.RoundToInt(Player.Instance.stats.damage * 0.1f);
        // Player.Instance.stats.damage += increaseAmount;

        Player.Instance.stats.damage++;
    }

    public void IncreaseMoveSpeed()
    {
        Player.Instance.stats.moveSpeed *= 1.2f; // Increase moveSpeed by 10%
    }

    public void IncreaseHealth()
    {
        int increaseAmount = Mathf.RoundToInt(Player.Instance.stats.maxHealth * 0.1f);
        Player.Instance.stats.maxHealth += increaseAmount;
        Player.Instance.stats.currentHealth = Player.Instance.stats.maxHealth;
    }

    public void DecreaseDashCooldown()
    {
        Player.Instance.stats.dashCooldown *= 0.8f; // Decrease dashCooldown by 10%
    }*/


/*    public void PauseGame()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.gemSound, 0.15f, 0.1f);
        if (!isPaused)
        {
            pauseScreen.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
        }
        else
        {
            pauseScreen.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
        }
    }*/
}
