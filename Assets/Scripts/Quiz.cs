using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<Question> questions = new List<Question>();
    Question question;
    private int lastQuestionIndex = -1;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    bool hasAnswered = true;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite wrongAnswerSprite;

    [Header("Loading")]
    private bool isLoadingNextQuestion;
    [SerializeField] float delayTimer;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;
    public bool isComplete;

    [Header("Progress Text")]
    [SerializeField] TextMeshProUGUI progressText;

    void Start()
    {
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
        GetNextQuestion();
    }

    void Update()
    {
        if (isLoadingNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }

            delayTimer -= Time.deltaTime;
            if (delayTimer <= 0f)
            {
                isLoadingNextQuestion = false;
                GetNextQuestion();
                SetButtonState(true);
            }
        }
    }

    void ShowQuestion()
    {
        questionText.text = question.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }
    }

    void GetRandomQuestion()
    {
        // Get a random question index different from the last question index
        int questionIndex = Random.Range(0, questions.Count);
        while (questionIndex == lastQuestionIndex)
        {
            questionIndex = Random.Range(0, questions.Count);
        }

        lastQuestionIndex = questionIndex;
        question = questions[questionIndex];
    }

    void ShowAnswer(int index)
    {
        Image buttonImage;

        if (index == question.GetCorrectAnswerIndex())
        {
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        else if (index != -1)
        {
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = wrongAnswerSprite;
        }
    }

    private void RemoveQuestionIfCorrect(int index)
    {
        Button button = answerButtons[index].GetComponent<Button>();
        button.enabled = false;

        if (index == question.GetCorrectAnswerIndex())
        {
            progressBar.value++;
            questions.Remove(question);
            UpdateProgressBar();
        }

        lastQuestionIndex = -1;
    }

    public void OnAnswerSelected(int index)
    {
        if (!hasAnswered)
        {
            hasAnswered = true;
            ShowAnswer(index);
            SetButtonState(false);
            RemoveQuestionIfCorrect(index);
            delayTimer = 2f; // Delay for 2 seconds before loading the next question
            isLoadingNextQuestion = true;
        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultSprite()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultSprite;
        }
    }

    public void GetNextQuestion()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.enabled = true;
        }

        if (questions.Count > 0)
        {
            GetRandomQuestion();
        }

        SetButtonState(true);
        SetDefaultSprite();
        ShowQuestion();
        hasAnswered = false;

        
    }

    void UpdateProgressBar()
    {
        float progressPercentage = (float)progressBar.value / (float)progressBar.maxValue;
        Debug.Log("Progress: " + (progressPercentage * 100f) + "%");
        int percentage = Mathf.RoundToInt(progressPercentage * 100f);
        progressText.text = percentage + "%";
    }
}


