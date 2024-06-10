using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FishType", menuName = "Fishing/FishType")]
public class FishType : ScriptableObject
{
    public string fishName;
    public int rarity; // Lower value means more common
    public string description;
}
