using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.gameObject.name == "GroundPoly")
        {
      

            PlayerController playerController = FindObjectOfType<PlayerController>();
            if (playerController != null)
            {
                playerController.CancelFishing(false);
            }
        }
    }
}
