using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    [SerializeField] GameObject winScreen;
   
    // Start is called before the first frame update
    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        
    }
    void Start()
    {
        quiz.gameObject.SetActive(true);
    }

    IEnumerator ActivateWinCanvas()
    {
        yield return new WaitForSeconds(2f); // Delay for 2 seconds
        quiz.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (quiz.isComplete) 
        {

            StartCoroutine(ActivateWinCanvas());

        }
    }
}
