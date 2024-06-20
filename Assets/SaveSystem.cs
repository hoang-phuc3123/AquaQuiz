using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class SaveSystem : MonoBehaviour
{
    public TMP_InputField inputField;
    [SerializeField] private GameObject player;
    private string playerX;
    private string playerY;
    private string playerZ;

    private void Update()
    {
        player = GameObject.Find("Player");
    }
    public void SaveData()
    {
        PlayerPrefs.SetString("Input", inputField.text);
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", player.transform.position.z);
        PlayerPrefs.SetInt("CurrentScene", SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Data Saved");
        playerX = PlayerPrefs.GetFloat("PlayerX").ToString();
        playerY = PlayerPrefs.GetFloat("PlayerY").ToString();
        playerZ = PlayerPrefs.GetFloat("PlayerZ").ToString();

        Debug.Log("Player Position: " + playerX + " " + playerY + " " + playerZ);

    }

    public void LoadData()
    {
        int currentScene = PlayerPrefs.GetInt("CurrentScene");
        SceneManager.LoadScene(currentScene);
        inputField.text = PlayerPrefs.GetString("Input");
        // Set the player position to the saved position
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteKey("Input");
        PlayerPrefs.DeleteAll();
    }
}