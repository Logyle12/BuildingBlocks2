using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// 1. The Grading States
public enum RoundResult
{
    Fail,
    Intermediate,
    Win
}

public class QuizManager : MonoBehaviour
{
    [Header("Quiz Data")]
    public TextAsset quizDataCSV;
    
    [Header("Progression")]
    public int starsEarned = 0; 
    private float[] starMultipliers = { 0.25f, 0.3125f, 0.4375f, 1.0f };

    // State Tracking (Public getters, private setters for safety)
    public int mistakesMade { get; private set; } = 0;
    public int correctAnswers { get; private set; } = 0;
    public int currentQuestionIndex { get; private set; } = 0;
    public int totalQuestionsThisRound { get { return selectedQuestions.Count; } }

    // Question Lists
    private List<QuestionData> allQuestions;
    private List<QuestionData> selectedQuestions = new List<QuestionData>();

    void Start()
    {
        // --- NEW LOAD LOGIC ---
        // Load the stars they already have for this specific stage 
        // so the question count multiplier is correct from the very start.
        string currentCategory = PlayerPrefs.GetString("CurrentCategoryPlaying", "Unknown");
        int currentStage = PlayerPrefs.GetInt("CurrentStagePlaying", 0);
        string saveKey = currentCategory + "_Stage_" + currentStage + "_Stars";
        
        starsEarned = PlayerPrefs.GetInt(saveKey, 0);
        // ----------------------

        allQuestions = CSVReader.ReadCSV(quizDataCSV);
        SetupNewRound();
    }

    public void SetupNewRound()
    {
        correctAnswers = 0;
        mistakesMade = 0;
        currentQuestionIndex = 0;
        SelectQuestionsByWeight();
    }

    private void SelectQuestionsByWeight()
    {
        selectedQuestions.Clear();
        
        int safeStarIndex = Mathf.Clamp(starsEarned, 0, starMultipliers.Length - 1);
        float currentMultiplier = starMultipliers[safeStarIndex];
        int countToAsk = Mathf.RoundToInt(allQuestions.Count * currentMultiplier);
        
        List<QuestionData> pool = new List<QuestionData>(allQuestions);
        
        for (int i = 0; i < countToAsk; i++)
        {
            float totalWeight = pool.Sum(q => q.weight);
            float randomValue = Random.Range(0, totalWeight);
            float runningSum = 0;

            foreach (var q in pool)
            {
                runningSum += q.weight;
                if (randomValue <= runningSum)
                {
                    selectedQuestions.Add(q);
                    pool.Remove(q); 
                    break;
                }
            }
        }
    }

    public QuestionData GetCurrentQuestion()
    {
        if (currentQuestionIndex < selectedQuestions.Count)
        {
            return selectedQuestions[currentQuestionIndex];
        }
        return null; 
    }

    public void ProcessAnswer(bool isCorrect)
    {
        if (isCorrect)
        {
            correctAnswers++;
        }
        else
        {
            mistakesMade++;
            selectedQuestions[currentQuestionIndex].weight *= 0.5f;
        }

        currentQuestionIndex++;
    }

    // THIS IS PUBLIC AND UNCOMMENTED
    public void EvaluateRound()
    {
        float percentageCorrect = (float)correctAnswers / selectedQuestions.Count;
        RoundResult finalResult;

        if (percentageCorrect >= 1.0f) 
        {
            finalResult = RoundResult.Win;
            foreach (var q in allQuestions) q.weight = 1.0f;
            if (starsEarned < 3) starsEarned++; // They earned a star!
        }
        else if (percentageCorrect > 0.5f) 
        {
            finalResult = RoundResult.Intermediate;
        }
        else 
        {
            finalResult = RoundResult.Fail;
        }

        // --- NEW SAVE LOGIC ---
        // 1. Get the sticky note of what level we are playing
        string currentCategory = PlayerPrefs.GetString("CurrentCategoryPlaying", "Unknown");
        int currentStage = PlayerPrefs.GetInt("CurrentStagePlaying", 0);

        // 2. Build the exact same string your LevelManager looks for
        string saveKey = currentCategory + "_Stage_" + currentStage + "_Stars";

        // 3. Check if they beat their high score for this level
        int previousStars = PlayerPrefs.GetInt(saveKey, 0);
        if (starsEarned > previousStars)
        {
            PlayerPrefs.SetInt(saveKey, starsEarned);
            PlayerPrefs.Save(); // Locks it in!
        }
        // -----------------------

        // BRIDGE: This tells the UI Manager to switch to the Win/Fail screen
        FindObjectOfType<QuizUIManager>().ShowEndScreen(finalResult);
    }
}