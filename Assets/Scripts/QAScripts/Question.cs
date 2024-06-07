using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionText;
    public string[] answers = new string[4]; // Array to store 4 answer options
    public int correctAnswerIndex; // Index (0-3) of the correct answer in the 'answers' array
}