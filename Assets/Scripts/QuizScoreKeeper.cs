using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizScoreKeeper : MonoBehaviour
{
    int totalCorrectAnswers = 0;
    int totalQuestionsSeen = 0;

    public int GetTotalCorrectAnswers()
    {
        return totalCorrectAnswers;
    }

    public void IncrementTotalCorrectAnswers()
    {
        totalCorrectAnswers++;
        
    }

    public int GetTotalQuestionsSeen()
    {
        return totalQuestionsSeen;
    }

    public void IncrementTotalQuestionsSeen()
    {
        totalQuestionsSeen++;
        
    }

    public int CalculateQuizScore()
    {
        if (totalQuestionsSeen == 0)
            return 0;

        float scorePercentage = (float)totalCorrectAnswers / totalQuestionsSeen * 100;
        return Mathf.RoundToInt(scorePercentage);
    }
}
