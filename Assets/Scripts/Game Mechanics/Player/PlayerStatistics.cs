using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatistics : MonoBehaviour
{
    public Image healthBar;
    public Image staminaBar;
    public Image manaBar;

    [SerializeField]
    public Image damageIndicator;
    private Animator myAnimator;

    // variable used to check if the health was increased or decreased
    private float tempHealth;

    private void Start()
    {
        myAnimator = damageIndicator.GetComponent<Animator>();
        tempHealth = GameManager.instance.currentHealth;
    }

    private void Update()
    {
        HealthChange();
        StaminaChange();
        ManaChange();
    }
    private void HealthChange()
    {
        float amount = Mathf.Clamp01(GameManager.instance.currentHealth / GameManager.instance.maxHealth);
        healthBar.fillAmount = amount;
        // if health was reduced indicate that damage was given
        if (tempHealth > GameManager.instance.currentHealth)
        {
            myAnimator.Play("DamageIndicator");
        }
        tempHealth = GameManager.instance.currentHealth;     
    }
    private void StaminaChange()
    {
        float amount = Mathf.Clamp01(GameManager.currentStamina / GameManager.maxStamina);
        staminaBar.fillAmount = amount;
    }

    private void ManaChange()
    {
        float amount = Mathf.Clamp01(GameManager.currentMana / GameManager.maxMana);
        manaBar.fillAmount = amount;
    }
}
