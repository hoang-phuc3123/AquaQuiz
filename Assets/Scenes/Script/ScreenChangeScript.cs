
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenChangeScript : MonoBehaviour
{
    public int sceneBuildIndex;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Could use other.GetComponent<Player>() to see if the game object has a Player component
        // Tags work too. Maybe some players have different script components?
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("SceneChangePositionX", playerPosition.x);
            PlayerPrefs.SetFloat("SceneChangePositionY", playerPosition.y);
            // Set flag to not use saved position
            PlayerPrefs.SetInt("UseSavedPosition", 0);
            LoadLoadingScene();
        }
    }
    private void LoadLoadingScene()
    {
        // Store the target scene index in a globally accessible location
        PlayerPrefs.SetInt("SceneToLoad", sceneBuildIndex);
        // Load the loading screen
        SceneManager.LoadScene("LoadingScene");
    }
}
