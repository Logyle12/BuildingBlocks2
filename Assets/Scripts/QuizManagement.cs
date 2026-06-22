using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class QuizManagement : MonoBehaviour
{

    //ref to background sprite
    public Image backgroundSprite;

    [SerializeField] TextMeshProUGUI choice;
    [SerializeField] string buttonText;


    [SerializeField] bool isAnswer = false;



    //start is a method which is called when the object to which script is assigned is active
    void Start()
    {

        //we get the button attached to the object 
        choice.text = buttonText;

    }

    //method which help us to identify if player has pressed correct or wrong answer
    public void checkTheTextofButton()
    {


        //we compare the tag of button with the answer assign to the button number by MathsAndAnswerScript script
        if (isAnswer)
        {
            backgroundSprite.color = new Color32(74, 169, 108, 255);



        }
        else
        {
            backgroundSprite.color = new Color32(255, 89, 34, 255);

        }



    }


    public bool getAnswerState()
    {

        return isAnswer;
    
    }

}
