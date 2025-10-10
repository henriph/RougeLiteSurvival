using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Application.targetFrameRate = 60;
        SetGameState(GameState.MENU);
    }

    void Update()
    {
        
    }

    public void SetGameState(GameState state)
    {
        IEnumerable<IGameStateListener> gameStateListeners = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IGameStateListener>();
        foreach(IGameStateListener gameStateListener in gameStateListeners)
        {
            gameStateListener.GameStateChangedCallBack(state);
        }
    }

    public void WaveCompletedCallBack()
    {
        if(Player.instance.HasLeveledUp())
        {
            SetGameState(GameState.WAVETRANSITION);
        }
        else
        {
            SetGameState(GameState.SHOP);
        }
    }
}


public interface IGameStateListener
{
    void GameStateChangedCallBack(GameState state);
}