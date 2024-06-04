
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenChangeScript : MonoBehaviour
{
    public int sceneBuildIndex;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger Entered");

        // Could use other.GetComponent<Player>() to see if the game object has a Player component
        // Tags work too. Maybe some players have different script components?
        if (other.CompareTag("Player"))
        {
            // Player entered, so move level
            print("Switching Scene to " + sceneBuildIndex);
            playerStorage.initialValue = playerPosition;    
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }
}
