using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoaderScript : MonoBehaviour
{
    public Slider progressBar; // Reference to the progress bar UI element
    public Text progressText;  // Reference to the progress text UI element

    void Start()
    {
        // Get the scene to load from PlayerPrefs
        int sceneToLoad = PlayerPrefs.GetInt("SceneToLoad");

        // Start the asynchronous operation
        StartCoroutine(LoadSceneAsync(sceneToLoad));
    }

    IEnumerator LoadSceneAsync(int sceneBuildIndex)
    {
        // Begin to load the scene you specified
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneBuildIndex);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting
        while (!asyncOperation.isDone)
        {
            // Output the current progress
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            progressBar.value = progress;
            progressText.text = (progress * 100f).ToString("F2") + "%";

            yield return null;
        }
    }
}
