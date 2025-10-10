using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLevel : MonoBehaviour
{
    [Header(" Settings ")]
    private float requireXp;
    private float currentXp;
    private int level;
    private int levelEarnedThisWave;

    [Header(" Visuals ")]
    [SerializeField] private Slider xpSlider;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Awake()
    {

        Candy.onCollected += CandyCollectedCallback;
    }

    private void OnDestroy()
    {
        Candy.onCollected -= CandyCollectedCallback;
    }

    void Start()
    {
        UpdateRequireXP();
        UpdateVisuals();
    }

    
    void Update()
    {
        
    }

    private void UpdateRequireXP()
    {
        requireXp = (level + 1) * 5;
    }

    private void UpdateVisuals()
    {
        xpSlider.value = currentXp / requireXp;
        levelText.text = "lvl " + (level + 1);
    }

    private void CandyCollectedCallback(Candy candy)
    {
        currentXp++;

        if (currentXp >= requireXp)
        {
            LevelUp();
        }

        UpdateVisuals();
    }

    private void LevelUp()
    {
        level++;
        levelEarnedThisWave++;        
        currentXp = 0;
        UpdateRequireXP();
    }

    public bool HasLeveledUp()
    {
        if (levelEarnedThisWave > 0)
        {
            levelEarnedThisWave--;
            return true;
        }

        return false;
    }
}
