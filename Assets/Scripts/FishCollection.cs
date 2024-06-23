using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class FishCollection : MonoBehaviour
{
    public static FishCollection Instance { get; private set; }
    public List<FishData> fishDataList;
    public GameObject fishCollectionUI;
    public GameObject fishPrefab;
    public PlayerController playerController;

    private Dictionary<FishData, bool> fishCaughtStatus = new Dictionary<FishData, bool>();
    private Dictionary<FishData, GameObject> fishCollectionObjects = new Dictionary<FishData, GameObject>();
    private bool isInitialized = false;

    void Start()
    {
        //fishCollectionUI.transform.parent.GetComponent<Canvas>().enabled = false;
        InitializeFishCollection();
        LoadFishData();  // Load fish data at start
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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

    public bool IsFishCaught(FishData fishData)
    {
        return fishCaughtStatus.ContainsKey(fishData) && fishCaughtStatus[fishData];
    }
    

    private void InitializeFishCollection()
    {
        //if (!isInitialized && fishCollectionUI != null) // Initialize only once and if UI is present
        //{
            foreach (FishData fishData in fishDataList)
            {
                //Debug.Log(fishData.fishName + " initialized");
                fishCaughtStatus[fishData] = false;

                GameObject fishObject = Instantiate(fishPrefab, fishCollectionUI.transform);
                fishCollectionObjects[fishData] = fishObject;

                FishCollectionItem fishCollectionItem = fishObject.GetComponent<FishCollectionItem>();
                //if (fishCollectionItem == null)
                //{
                //    Debug.LogError("FishCollectionItem component not found on fishObject: " + fishObject.name);
                //    continue;
                //}
                fishCollectionItem.Initialize(fishData);

                // Add an EventTrigger component if not already added
                EventTrigger eventTrigger = fishObject.GetComponent<EventTrigger>();
                //if (eventTrigger == null)
                //{
                //    eventTrigger = fishObject.AddComponent<EventTrigger>();
                //    Debug.Log("EventTrigger component added to " + fishObject.name);
                //}

                // Clear existing triggers to avoid duplicates
                eventTrigger.triggers.Clear();

                // Create and add the pointer click event entry
                EventTrigger.Entry pointerClickEntry = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.PointerClick
                };
                pointerClickEntry.callback.AddListener((eventData) => { fishCollectionItem.OnFishClicked(eventData); });
                eventTrigger.triggers.Add(pointerClickEntry);
                //Debug.Log("PointerClick event added to EventTrigger for " + fishObject.name);
            }
            //foreach (var fishObject in fishCollectionObjects.Values)
            //{
            //    EventTrigger trigger = fishObject.GetComponent<EventTrigger>();
            //    if (trigger != null)
            //    {
            //        Debug.Log(fishObject.name + " has " + trigger.triggers.Count + " triggers.");
            //    }
            //}
            //isInitialized = true;
        //}
    }
    public void CloseCollectionUI()
    {
        if (fishCollectionUI == null)
        {
            Debug.LogError("fishCollectionUI is null! Cannot close the UI.");
            return; // Stop the method if the UI is null
        }
        fishCollectionUI.transform.parent.GetComponent<Canvas>().enabled = false;
        PlayerController player = FindObjectOfType<PlayerController>();

        if (player != null)
        {
            player.EnableInput();
        }
    }

    public void MarkFishAsCaught(FishData fishData)
    {
        if (fishCaughtStatus.ContainsKey(fishData))
        {
            fishCollectionObjects[fishData].GetComponent<FishCollectionItem>().RevealFish(fishData);
            fishCaughtStatus[fishData] = true;
        }
    }

    // Save fish data to PlayerPrefs
    public void SaveFishData()
    {
        List<FishDataWrapper> fishDataList = new List<FishDataWrapper>();

        foreach (var kvp in fishCaughtStatus)
        {
            if (kvp.Value) // Only save if the fish is caught
            {
                FishDataWrapper wrapper = new FishDataWrapper
                {
                    fishData = kvp.Key,
                    isCaught = kvp.Value
                };
                fishDataList.Add(wrapper);
            }
        }

        string json = JsonUtility.ToJson(new FishDataWrapperList(fishDataList));
        PlayerPrefs.SetString("FishCaughtStatus", json);
        PlayerPrefs.Save();
    }

    // Load fish data from PlayerPrefs
    public void LoadFishData()
    {
        if (PlayerPrefs.HasKey("FishCaughtStatus"))
        {
            string json = PlayerPrefs.GetString("FishCaughtStatus");
            FishDataWrapperList wrapperList = JsonUtility.FromJson<FishDataWrapperList>(json);
            fishCaughtStatus.Clear();

            foreach (var wrapper in wrapperList.fishDataList)
            {
                fishCaughtStatus[wrapper.fishData] = wrapper.isCaught;

                if (wrapper.isCaught)
                {
                    fishCollectionObjects[wrapper.fishData].GetComponent<FishCollectionItem>().RevealFish(wrapper.fishData);
                }
            }
        }
    }

    public void ClearFishData()
    {
        //fishCaughtStatus.Clear();
        foreach (var fishObject in fishCollectionObjects.Values)
        {
            Destroy(fishObject);
        }
        //fishCollectionObjects.Clear();
        PlayerPrefs.DeleteKey("FishCaughtStatus");
    }
}

[System.Serializable]
public class FishDataWrapper
{
    public FishData fishData;
    public bool isCaught;
}

[System.Serializable]
public class FishDataWrapperList
{
    public List<FishDataWrapper> fishDataList;

    public FishDataWrapperList(List<FishDataWrapper> fishDataList)
    {
        this.fishDataList = fishDataList;
    }
}



