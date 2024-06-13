using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishManager : MonoBehaviour
{
    public LocationFishData[] sceneFishData;

    public FishData GetRandomFish()
    {
        // Get the current scene name
        string currentScene = SceneManager.GetActiveScene().name;

        // Find the SceneFishData for the current scene
        LocationFishData sceneData = System.Array.Find(sceneFishData, data => data.locationName == currentScene);

        if (sceneData != null)
        {
            if (sceneData.fish.Length > 0)
            {
                int randomIndex = Random.Range(0, sceneData.fish.Length);
                return sceneData.fish[randomIndex];
            }
            else
            {
                Debug.LogError("No fish assigned for scene: " + currentScene);
                return null;
            }
        }
        else
        {
            Debug.LogError("No SceneFishData found for scene: " + currentScene);
            return null;
        }
    }
}
