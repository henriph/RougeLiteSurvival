using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Slider healthSlider;

    [Header(" Settings ")]
    [SerializeField] private int maxHealth;
    private int health;

    private void Start()
    {
        health = maxHealth;
        healthSlider.value = 1;
    }
    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(health, damage);
        health -= realDamage;
        float healthSlidervalue = (float) health / maxHealth;
        healthSlider.value = healthSlidervalue;

        if (health <= 0)
        {
            PassAway();
        }
    }

    private void PassAway()
    {
        Debug.Log("Ded");
    }
}
