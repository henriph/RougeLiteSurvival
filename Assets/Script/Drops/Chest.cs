using System;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header(" Actions ")]
    public static Action onCollected;

    public void Collect(Transform playerTransform)
    {
        onCollected?.Invoke();
        Destroy(gameObject);
    }
}
