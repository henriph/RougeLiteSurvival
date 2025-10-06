using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Droppable : MonoBehaviour, ICollectable
{
    protected bool collected;

    private void OnEnable()
    {
        collected = false;
    }
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

    protected abstract void Collected();

}
