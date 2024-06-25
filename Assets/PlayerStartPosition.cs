using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPosition : MonoBehaviour
{
    public static Vector3? TargetPosition = null;

    void Start()
    {
        Debug.Log("PlayerPositionManager Start called.");

        if (TargetPosition.HasValue)
        {
            Debug.Log("TargetPosition set to: " + TargetPosition.Value);
            // Delay setting the position to ensure player is fully loaded
            StartCoroutine(SetPlayerPosition());
        }
        else
        {
            Debug.Log("No target position set.");
        }
    }

    private IEnumerator SetPlayerPosition()
    {
        yield return new WaitForEndOfFrame(); // Wait for the end of the frame to ensure everything is loaded

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = TargetPosition.Value;
            Debug.Log("Player position set to: " + TargetPosition.Value);
            TargetPosition = null; // Clear the target position after setting it
        }
        else
        {
            Debug.LogError("Player object not found!");
        }
    }
}
