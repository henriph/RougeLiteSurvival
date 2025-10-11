using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header(" Settings ")]
    [SerializeField] private int maxHealth;
    private int health;

    private void Start()
    {
        health = maxHealth;
        UpdateHealthBarUI();
    }
    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(health, damage);
        health -= realDamage;

        UpdateHealthBarUI();

        if (health <= 0)
            PassAway();
    }

    private void PassAway()
    {
        GameManager.instance.SetGameState(GameState.GAMEOVER);
    }

    private void UpdateHealthBarUI()
    {
        float healthSlidervalue = (float)health / maxHealth;
        healthSlider.value = healthSlidervalue;
        healthText.text = health + " / " + maxHealth;
    }
}
