using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class FishCollectionItem : MonoBehaviour
{
    public Image fishImage;  // Or SpriteRenderer 
    public TMP_Text fishNameText;
    public FishInfoDisplay fishInfoDisplay;
    private FishData fishData;

    public void Initialize(FishData fishData)
    {
        this.fishData = fishData;
        fishImage.sprite = fishData.fishSprite;
        fishImage.color = Color.black; // Set to black for the silhouette effect
        fishNameText.text = "???";
        if (fishInfoDisplay == null)
        {
            fishInfoDisplay = FindObjectOfType<FishInfoDisplay>();
            //if (fishInfoDisplay == null)
            //{
            //    Debug.LogError("FishInfoDisplay is not found in the scene. Please assign it in the inspector or make sure it's present in the scene.");
            //}
        }

    }

    public void RevealFish(FishData fishData)
    {
        fishImage.color = Color.white;   // Restore original sprite colors
        fishNameText.text = fishData.fishName;
    }
    public void OnFishClicked(BaseEventData eventData)
    {
        if (fishData != null) // Now you can access fishData here
        {
            if (FishCollection.Instance != null) // Ensure FishCollectionManager is found
            {
                // Check if the fish has been caught:
                if (FishCollection.Instance.IsFishCaught(fishData))
                {
                    fishInfoDisplay = FindObjectOfType<FishInfoDisplay>();
                    if (fishInfoDisplay != null) // Ensure fishInfoDisplay is assigned
                    {
                        fishInfoDisplay.ShowFishInfo(fishData);
                    }
                }
            }
        }
    }
}
