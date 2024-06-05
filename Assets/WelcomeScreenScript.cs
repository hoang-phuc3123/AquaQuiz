using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeScreenScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    public void ExitButton()
    {
        Application.Quit();
    }
}
