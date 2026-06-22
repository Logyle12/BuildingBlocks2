using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoiceText : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI question;
    [SerializeField] string questionText;

    void Start()
    {
        question.text = questionText;
    }



}
