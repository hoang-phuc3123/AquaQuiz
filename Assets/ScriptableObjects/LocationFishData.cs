using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Location Fish Data", menuName = "Location Fish Data")]
public class LocationFishData : ScriptableObject
{
    public string locationName;
    public FishData[] fish;
}
