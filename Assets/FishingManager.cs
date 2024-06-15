using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine;

public class FishingManager : MonoBehaviour
{

    public Canvas fishingCanvas;
    public Slider progressBar;
    public TMP_Text questionText;
    public Button[] answerButtons;
    public QuestionGenerator questionGenerator;
    public FishManager fishManager;
    public FishInfoDisplay fishInfo;
    public FishCollection fishCollection;

    [SerializeField] private PlayerController playerController;

    private Question currentQuestion;
    private float progress; // Start at 30%
    private float timeDecreaseRate = 5f; // Adjust how fast it decrease
    private bool isFishing = false;

    // Start is called before the first frame update
    void Start()
    {
        fishingCanvas.enabled = false;
        //fishInfo.fishInfoCanvas.enabled = false;
        fishCollection = FindObjectOfType<FishCollection>();

        if (fishCollection == null)
        {
            Debug.LogError("FishCollection not found in the scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //private void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}

    private IEnumerator FishingMinigameLoop()
    {

        while (progress > 0 && progress < 100 && isFishing)
        {
            // Decrease progress over time
            progress -= timeDecreaseRate * Time.deltaTime;
            progressBar.value = progress;
            
            // End fishing if progress goes below 0
            if (progress <= 0)
            {
                Debug.Log("Time's up! The fish got away!");
                EndFishing();
                yield break;
            }

            yield return null;
        }
        if (isFishing && progress >= 100) // Check isFishing to avoid calling EndFishing twice
        {
            
            FishData caughtFish = fishManager.GetRandomFish();
            FishCollection.Instance.MarkFishAsCaught(caughtFish);
            fishInfo.ShowFishInfo(caughtFish);
            EndFishingWaitForCanvas();
            yield return new WaitUntil(() => !fishInfo.fishInfoCanvas.enabled);
            playerController.EnableInput();
        }
        if (isFishing && progress <= 0)
        {
            Debug.Log("Time's up! The fish got away!");
            EndFishing();
        }

    }


    public void StartFishing()
    {
        // Enable the fishing canvas
        isFishing = true;
        progress = 30f;
        fishingCanvas.enabled = true;

        currentQuestion = questionGenerator.GetRandomQuestion();
        DisplayQuestion();

        StartCoroutine(FishingMinigameLoop()); // Start the coroutine correctly
    }
    private void DisplayQuestion()
    {
        if (currentQuestion != null)
        {
            questionText.text = currentQuestion.questionText;

            // Store the original correct answer *value* before shuffling
            string correctAnswerValue = currentQuestion.answers[currentQuestion.correctAnswerIndex];

            // Shuffle the answers array using Fisher-Yates Shuffle
            System.Random random = new System.Random();
            int n = currentQuestion.answers.Length;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                string temp = currentQuestion.answers[k];
                currentQuestion.answers[k] = currentQuestion.answers[n];
                currentQuestion.answers[n] = temp;
            }

            // Find the NEW index of the correct answer after shuffling:
            currentQuestion.correctAnswerIndex = System.Array.IndexOf(currentQuestion.answers, correctAnswerValue);

            for (int i = 0; i < answerButtons.Length; i++)
            {
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].GetComponentInChildren<TMP_Text>().text = currentQuestion.answers[i];
                int index = i;
                answerButtons[i].onClick.AddListener(() => PlayerChoose(index));
            }
        }
        else
        {
            Debug.LogError("Current question is null in FishingManager!");
        }
    }
    private void PlayerChoose(int buttonIndex)
    {
        if (buttonIndex == currentQuestion.correctAnswerIndex)
        {
            progress += 20f;
        }
        else
        {
            progress -= 10f;
        }
        currentQuestion = questionGenerator.GetRandomQuestion();
        DisplayQuestion();
    }

    // End the fishing minigame
    public void EndFishing()
    {
        isFishing = false;
        fishingCanvas.enabled = false;
        //playerController.EnableInput();
        playerController.CancelFishing(false);
    }
    public void EndFishingWaitForCanvas()
    {
        isFishing = false;
        fishingCanvas.enabled = false;
        playerController.CancelFishing(true);
    }
    
}
