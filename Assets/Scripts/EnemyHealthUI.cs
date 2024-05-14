using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{

    [SerializeField] private Slider healthSlider;
    private Health health;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSlider != null & health.currentHealth > 0)
        {
            healthSlider.value = health.currentHealth;
            healthSlider.maxValue = health.maxHealth;
        }
    }
}
