using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [Header(" Colliders ")]
    [SerializeField] private CircleCollider2D collectableCollider;
    [SerializeField] private BoxCollider2D playerCollider;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Check if the object entering the trigger is the Candy component.
        if (collider.TryGetComponent(out ICollectable collectable))
        {
            if (!collider.IsTouching(collectableCollider))
                return;

            //The trigger event already confirms contact.
            collectable.Collect(transform);
        } else if(collider.TryGetComponent(out Chest chest))
        {
            if (!collider.IsTouching(playerCollider))
                return;

            chest.Collect(transform);
        }
    }
}
