using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(WaveManagerUI))]
public class WaveManager : MonoBehaviour, IGameStateListener
{
    [Header(" Elements ")]
    [SerializeField] private Player player;
    private WaveManagerUI ui;

    [Header(" Settings ")]
    [SerializeField] private float waveDuration;
    private float timer;
    private bool isTimerOn;
    private int currentWaveIndex;

    [Header(" Waves ")]
    [SerializeField] private Wave[] waves;
    private List<float> localCounters = new List<float>();

    private void Awake()
    {
        ui = GetComponent<WaveManagerUI>();
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        if (!isTimerOn)
        {
            return;
        }

        if (timer < waveDuration)
        {
            ManageCurrentWave();

            string timerString = ((int)(waveDuration - timer)).ToString();
            ui.UpdateTimerText(timerString);
        } else
        {
            StartWaveTransition();
        }
    }

    private void StartWave(int waveIndex)
    {
        Debug.Log("Start Wave " +  waveIndex);
        string waveString = "Wave " + (waveIndex + 1).ToString() + " / " + waves.Length.ToString();
        ui.UpdateWaveText(waveString);

        localCounters.Clear();
        foreach (WaveSegment segment in waves[waveIndex].segments) {
            localCounters.Add(1);
        }

        timer = 0;
        isTimerOn = true;
    }

    private void StartWaveTransition()
    {
        isTimerOn = false;

        DefeatAllEnemies();
        currentWaveIndex++;

        if (currentWaveIndex >= waves.Length)
        {
            Debug.Log("Waves completed!");
            GameManager.instance.SetGameState(GameState.STAGECOMPLETE);
        }
        else
        {
            GameManager.instance.WaveCompletedCallBack();
        }
    }

    private void StartNextWave()
    {
        StartWave(currentWaveIndex);
    }

    private void ManageCurrentWave()
    {
        Wave currentWave = waves[currentWaveIndex];

        for (int i = 0; i < currentWave.segments.Count; i++)
        {
            WaveSegment segment = currentWave.segments[i];

            float tStart = segment.tStartEnd.x / 100 * waveDuration;
            float tEnd = segment.tStartEnd.y / 100 * waveDuration;

            if (tStart > timer || timer > tEnd)
            {
                continue;
            }
            float timeSinceSegmentStart = timer - tStart;

            float spawnDelay = 1f / segment.spawnFrequency;


            if(timeSinceSegmentStart / spawnDelay > localCounters[i])
            {
                Instantiate(segment.prefab, GetSpawnPosition(), Quaternion.identity, transform);
                localCounters[i]++;
            }
        }

        timer += Time.deltaTime;
    }

    private Vector2 GetSpawnPosition()
    {
        Vector2 playerPos = (Vector2)player.transform.position;

        // The maximum visible distance is 6.92 (Orthographic Size).
        // MIN_SPAWN_RADIUS must be LARGER than the camera's half-height or half-width.
        // Let's use 8.0f as the guaranteed off-screen minimum.
        const float MIN_SPAWN_RADIUS = 15.0f;

        // Define a random buffer to spread enemies out
        const float RANDOM_BUFFER = 5.0f;

        // Calculate a random distance: 8 units (guaranteed off-screen) to 13 units
        float spawnDistance = MIN_SPAWN_RADIUS + Random.Range(0f, RANDOM_BUFFER);

        // Get a random direction on the unit circle
        Vector2 direction = Random.onUnitSphere.normalized;

        // Calculate the final position: PlayerPos + (RandomDirection * SpawnDistance)
        return playerPos + direction * spawnDistance;
    }

    private void DefeatAllEnemies()
    {
        while (transform.childCount > 0)
        {
            Transform child = transform.transform.GetChild(0);
            child.SetParent(null);
            Object.Destroy(child.gameObject);
        }
    }

    public void GameStateChangedCallBack(GameState state)
    {
        switch(state)
        {
            case GameState.MENU:
                break;
            case GameState.GAME:
                StartNextWave();
                break;
            case GameState.WAVETRANSITION:
                break;
            case GameState.SHOP:
                break;
            case GameState.GAMEOVER:
                isTimerOn = false;
                DefeatAllEnemies();
                break;
        }
    }
}

[System.Serializable]
public struct Wave
{
    public string name;
    public List<WaveSegment> segments;

}

[System.Serializable]
public struct WaveSegment
{
    public Vector2 tStartEnd;
    public float spawnFrequency;
    public GameObject prefab;
}
