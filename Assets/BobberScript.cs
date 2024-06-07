using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberScript : MonoBehaviour
{
    public PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider is a CompositeCollider2D and has the tag "Ground" 
        if (other.TryGetComponent<CompositeCollider2D>(out var compositeCollider) &&
            other.gameObject.CompareTag("Ground"))
        {
            playerController.CancelFishing();
        }
    }

}
