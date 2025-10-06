using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [Header(" Colliders ")]
    [SerializeField] private CircleCollider2D playerCollider;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Check if the object entering the trigger is the Candy component.
        if (collider.TryGetComponent(out Candy candy))
        {
            if (!collider.IsTouching(playerCollider))
                return;

            //The trigger event already confirms contact.
            Debug.Log("Collect candy!");
            candy.Collect(transform);
        }
    }

    private void CandyDetection()
    {
    }
}
