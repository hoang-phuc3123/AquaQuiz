using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fish Data", menuName = "Fish Data")]
public class FishData : ScriptableObject
{
    public string fishName;
    public int rarity; 
    public string description;
    public Sprite fishSprite;
    public Texture2D fishImage;
}
