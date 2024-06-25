using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System.Collections;

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
        if (FishCollection.Instance != null)
        {
            FishCollection.Instance.SaveFishData();
        }
    }

    public void LoadData()
    {
        // Set flag to use saved position
        PlayerPrefs.SetInt("UseSavedPosition", 1);
        if (FishCollection.Instance != null)
        {
            FishCollection.Instance.LoadFishData();
        }
        int currentScene = PlayerPrefs.GetInt("CurrentScene");
        StartCoroutine(LoadSceneAndSetPosition(currentScene));
    }

    private IEnumerator LoadSceneAndSetPosition(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        inputField.text = PlayerPrefs.GetString("Input");
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteKey("Input");
        PlayerPrefs.DeleteAll();
    }
}