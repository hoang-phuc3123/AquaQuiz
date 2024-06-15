using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPersistence : MonoBehaviour
{
    public static CanvasPersistence Instance { get; private set; }
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Only destroy if this is a different instance
        }
    }
}
