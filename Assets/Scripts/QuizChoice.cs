using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizChoice : MonoBehaviour
{
    public static QuizChoice instance;

    public string answer;
    void Awake()
    {
        MakeInstance();
    }

    //method which make this object instance
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

     void Start()
    {
        answer = "Button";

    }
}
