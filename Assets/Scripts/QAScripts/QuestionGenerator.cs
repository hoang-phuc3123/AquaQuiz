using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionGenerator : MonoBehaviour
{
    public Question[] questions;

    public Question GetRandomQuestion()
    {
        if (questions.Length > 0)
        {
            int randomIndex = Random.Range(0, questions.Length);
            return questions[randomIndex];
        }
        else
        {
            Debug.LogError("No questions available in QuestionGenerator!");
            return null;
        }
    }
}
