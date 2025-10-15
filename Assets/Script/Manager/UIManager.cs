using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class UIManager : MonoBehaviour, IGameStateListener
{
    [Header(" Panels ")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject stageCompletePanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject waveTransitionPanel;
    [SerializeField] private GameObject weaponSelectionPanel;
    [SerializeField] private GameObject gameOverPanel;

    private List<GameObject> panels = new List<GameObject>();

    private void Awake()
    {
        panels.AddRange(new GameObject[]
        {
            menuPanel,
            gamePanel,
            shopPanel,
            waveTransitionPanel,
            weaponSelectionPanel,
            gameOverPanel,
            stageCompletePanel
        });
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void GameStateChangedCallBack(GameState state)
    {
        switch (state)
        {
            case GameState.MENU:
                ShowPanel(menuPanel);
                break;
            case GameState.GAMEOVER:
                ShowPanel(gameOverPanel);
                break;
            case GameState.WEAPONSELECTION:
                ShowPanel(weaponSelectionPanel);
                break;
            case GameState.GAME:
                ShowPanel(gamePanel);
                break;
            case GameState.STAGECOMPLETE:
                ShowPanel(stageCompletePanel);
                break;
            case GameState.WAVETRANSITION:
                ShowPanel(waveTransitionPanel);
                break;
            case GameState.SHOP:
                ShowPanel(shopPanel);
                break;
        }
    }

    private void ShowPanel(GameObject panel, bool hidePrevousPanel = true)
    {
        foreach(GameObject p in panels)
        {
            if (p == panel)
            {
                p.SetActive(true);
            } else
            {
                p.SetActive(false);
            }
        }
    }
}
