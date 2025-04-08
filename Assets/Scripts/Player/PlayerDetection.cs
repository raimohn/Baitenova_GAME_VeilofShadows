using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerDetection : MonoBehaviour
{
    [Header(" Colliders ")]
    [SerializeField] private CircleCollider2D collectableCollider;

    /* private void FixedUpdate()
    {
        Collider2D[] essenceColliders = Physics2D.OverlapCircleAll(
            (Vector2)transform.position + playerCollider.offset,
            playerCollider.radius);

        foreach (Collider2D collider in essenceColliders)
        {
            if (collider.TryGetComponent(out Essence essence))
            {
                Debug.Log("Collected :" + essence.name);
                essence.Collect(GetComponent<Player>());
            }
        }
    }
    */

    private void OnTriggerEnter2D(Collider2D collider)
     {
         if(collider.TryGetComponent(out ICollectable collectable))
         {
             if(!collider.IsTouching(collectableCollider))
                 return;

             collectable.Collect(GetComponent<Player>());
         }
    }
}