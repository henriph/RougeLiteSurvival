using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Random = UnityEngine.Random;
using System;
public class WaveTransitionManager : MonoBehaviour, IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private Button[] upgradeContainers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStateChangedCallBack(GameState state)
    {
        switch (state)
        {
            case GameState.WAVETRANSITION:
                ConfigureUpgradeContainer();
                break;
        }
    }

    private void ConfigureUpgradeContainer()
    {
        for (int i = 0; i < upgradeContainers.Length; i++)
        {
            string randomStatString = "";
            int randomIndex = Random.Range(0, Enum.GetValues(typeof(Stat)).Length);

            Stat stat = (Stat)Enum.GetValues(typeof(Stat)).GetValue(randomIndex);

            randomStatString = Enums.FormatStatName(stat);

            upgradeContainers[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = randomStatString;
            upgradeContainers[i].Button.RemoveAllListeners();
        }
    }
}
