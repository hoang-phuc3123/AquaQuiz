using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialValue : MonoBehaviour
{

    public VectorValue startingPosition;

    void Start()
    {
        bool useSavedPosition = PlayerPrefs.GetInt("UseSavedPosition", 0) == 1;

        if (useSavedPosition && PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ"))
        {
            // Load saved position
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");
            transform.position = new Vector3(x, y, z);
        }
        else
        {
            // Use initial position from startingPosition
            transform.position = new Vector2 (PlayerPrefs.GetFloat("SceneChangePositionX"), PlayerPrefs.GetFloat("SceneChangePositionY"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
