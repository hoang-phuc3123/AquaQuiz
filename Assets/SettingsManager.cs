using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;
    private PlayerController playerController;

    void Start()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        // Tìm script điều khiển người chơi
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettingsPanel();
        }
    }

    void ToggleSettingsPanel()
    {
        if (settingsPanel != null)
        {
            bool isActive = settingsPanel.activeSelf;
            settingsPanel.SetActive(!isActive);

            // Cập nhật trạng thái của playerController
            if (playerController != null)
            {
                playerController.SetPaused(!isActive);
            }
        }
    }

}
