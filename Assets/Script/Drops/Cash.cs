using System.Collections;
using UnityEngine;

public class Cash : MonoBehaviour, ICollectable
{
    private bool collected;

    public void Collect(Transform playerTransform)
    {
        if (collected)
            return;

        collected = true;

        StartCoroutine(MoveTowardPlayer(playerTransform));
    }

    IEnumerator MoveTowardPlayer(Transform playerTransform)
    {
        float timer = 0;
        Vector2 initialPoint = transform.position;

        while (timer < 1)
        {
            transform.position = Vector2.Lerp(initialPoint, playerTransform.position, timer);
            timer += Time.deltaTime;
            yield return null;
        }


        Collected();
    }

    private void Collected()
    {
        gameObject.SetActive(false);
    }
}
