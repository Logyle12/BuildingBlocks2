using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToCompleteAnswer = 10f;

    float timervalue;
    public float timerFraction;

    public bool isAnsweringQuestion = false;
    public bool loadNextQuestion;
    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer() 
    {

        timervalue = 0f;
    
    }

    void UpdateTimer() 
    {
        timervalue -= Time.deltaTime;

        if(isAnsweringQuestion)
        {
            
            if(timervalue > 0) 
            {

                timerFraction = timervalue / timeToCompleteQuestion;
            
            }

            else
            {
                isAnsweringQuestion = false;
                timervalue = timeToCompleteAnswer;

            }

        }
        else 
        {

            if (timervalue > 0)
            {
                timerFraction = timervalue / timeToCompleteAnswer;
            }

            else 
            {

                timervalue = timeToCompleteQuestion;
                isAnsweringQuestion = true;
                loadNextQuestion = true;

            }
        }
        
     
    
    
    }
}
