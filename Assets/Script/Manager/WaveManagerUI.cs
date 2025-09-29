using TMPro;
using UnityEngine;

public class WaveManagerUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] TextMeshProUGUI timerText;

    public void UpdateWaveText(string waveString)
    {
        waveText.text = waveString;
    }

    public void UpdateTimerText(string timerString) { 
        timerText.text = timerString;
    }
}
