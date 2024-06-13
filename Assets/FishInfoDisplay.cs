using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishInfoDisplay : MonoBehaviour
{
    public Canvas fishInfoCanvas;
    public RawImage fishImage;
    public TMP_Text fishNameText;
    public TMP_Text fishDescriptionText;
    public Button button;

    [SerializeField] public PlayerController playerController;


    public void ShowFishInfo(FishData fish)
    {
        fishInfoCanvas.enabled = true;
        fishNameText.text = fish.fishName;
        fishImage.texture = fish.fishImage;
        fishDescriptionText.text = fish.description;
        playerController.DisableInput();
    }
    public void CloseFishInfo()
    {
        fishInfoCanvas.enabled = false;
        playerController.EnableInput();
    }
    public void EnableFishInfoCanvas()
    {
        fishInfoCanvas.enabled = true;
        playerController.DisableInput();
    }
    private void EnablePlayerInput()
    {
        playerController.EnableInput();
        fishInfoCanvas.enabled = false;
    }
}
