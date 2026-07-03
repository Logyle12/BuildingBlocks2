using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public enum RoundResult { Fail, Intermediate, Win }

public class QuizManager : MonoBehaviour
{
    [Header("Quiz Data")]
    public TextAsset quizDataCSV;
    
    [Header("Progression")]
    public int starsEarned = 0; 
    private float[] starMultipliers = { 0.25f, 0.3125f, 0.4375f, 1.0f };

    public int correctAnswers { get; private set; } = 0;
    public int currentQuestionIndex { get; private set; } = 0;
    public int totalQuestionsThisRound { get { return selectedQuestions.Count; } }

    private string levelSaveKey;
    private List<QuestionData> allQuestions;
    private List<QuestionData> selectedQuestions = new List<QuestionData>();

    void Start()
    {
        InitializeQuiz();
    }

    public void InitializeQuiz()
    {
        correctAnswers = 0;
        currentQuestionIndex = 0;
        selectedQuestions.Clear();

        string currentCategory = PlayerPrefs.GetString("CurrentCategoryPlaying", "Unknown");
        int currentStage = PlayerPrefs.GetInt("CurrentStagePlaying", 0);
        levelSaveKey = currentCategory + "_Stage_" + currentStage;
        
        starsEarned = PlayerPrefs.GetInt(levelSaveKey + "_Stars", 0);
        allQuestions = CSVReader.ReadCSV(quizDataCSV);

        int index = Mathf.Clamp(starsEarned, 0, starMultipliers.Length - 1);
        float multiplier = starMultipliers[index];
        int targetCount = Mathf.CeilToInt(allQuestions.Count * multiplier);

        List<QuestionData> availableQuestions = allQuestions;
        if (starsEarned < 3)
        {
            List<int> completedIds = GetCompletedIds(levelSaveKey);
            availableQuestions = allQuestions.Where(q => !completedIds.Contains(q.id)).ToList();
        }

        Shuffle(availableQuestions);
        selectedQuestions = availableQuestions.Take(targetCount).ToList();

        // FIX: Shuffle options so the answer isn't always Option A
        foreach (var q in selectedQuestions)
        {
            ShuffleOptions(q);
        }
    }

    private void ShuffleOptions(QuestionData q)
    {
        string correctAnswerText = q.options[q.correctAnswerIndex];
        System.Random rng = new System.Random();
        q.options = q.options.OrderBy(x => rng.Next()).ToArray();
        q.correctAnswerIndex = Array.IndexOf(q.options, correctAnswerText);
    }

    private void Shuffle<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1) { n--; int k = rng.Next(n + 1); T value = list[k]; list[k] = list[n]; list[n] = value; }
    }

    public void ProcessAnswer(bool isCorrect)
    {
        if (isCorrect)
        {
            correctAnswers++;
            QuestionData q = GetCurrentQuestion();
            if (q != null) SaveCompletedId(levelSaveKey, q.id);
        }
        currentQuestionIndex++;
    }

    private List<int> GetCompletedIds(string key)
    {
        string ids = PlayerPrefs.GetString(key + "_Completed", "");
        if (string.IsNullOrEmpty(ids)) return new List<int>();
        return ids.Split(',').Select(int.Parse).ToList();
    }

    private void SaveCompletedId(string key, int id)
    {
        List<int> completed = GetCompletedIds(key);
        if (!completed.Contains(id))
        {
            completed.Add(id);
            PlayerPrefs.SetString(key + "_Completed", string.Join(",", completed));
            PlayerPrefs.Save();
        }
    }

    public QuestionData GetCurrentQuestion()
    {
        return (currentQuestionIndex < selectedQuestions.Count) ? selectedQuestions[currentQuestionIndex] : null;
    }

    public void EvaluateRound()
    {
        float percentageCorrect = (float)correctAnswers / selectedQuestions.Count;
        RoundResult finalResult = (percentageCorrect >= 1.0f) ? RoundResult.Win : 
                                  (percentageCorrect > 0.5f) ? RoundResult.Intermediate : RoundResult.Fail;

        if (finalResult == RoundResult.Win && starsEarned < 3) 
        {
            starsEarned++;
            PlayerPrefs.SetInt(levelSaveKey + "_Stars", starsEarned);
            if (starsEarned == 3) PlayerPrefs.DeleteKey(levelSaveKey + "_Completed");
            PlayerPrefs.Save();
        }

        FindObjectOfType<QuizUIManager>().ShowEndScreen(finalResult);
    }
}