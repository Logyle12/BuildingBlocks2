using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class QuizUIManager : MonoBehaviour
{
    [Header("Manager References")]
    public QuizManager quizManager;

    [Header("Gameplay UI")]
    public GameObject gameplayCanvas;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI[] optionButtonTexts;
    public Button[] optionButtons;

    [Header("End Screen Panels")]
    public GameObject winPanel;
    public GameObject failPanel;

    [Header("Play Again")]
    public Button playAgainButton; // Assign this in the Inspector

    [Header("Progress")]
    public Slider progressBar;
    public TextMeshProUGUI progressText;

    [Header("Feedback Sprites")]
    public Sprite defaultSprite;
    public Sprite correctSprite;
    public Sprite wrongSprite;

    [Header("Timing")]
    [SerializeField] float delayBetweenQuestions = 1.5f;

    private bool isProcessingInput = false;

    void Start()
    {
        if (playAgainButton != null)
            playAgainButton.onClick.AddListener(OnPlayAgainClicked);

        ResetUIState();
        InitializeButtonListeners();
        StartCoroutine(InitializeUI());
    }

    private void ResetUIState()
    {
        if (gameplayCanvas != null) gameplayCanvas.SetActive(true);
        if (winPanel != null) winPanel.SetActive(false);
        if (failPanel != null) failPanel.SetActive(false);
    }

    public void OnPlayAgainClicked()
    {
        // 1. Reset logic and questions in manager
        quizManager.InitializeQuiz();

        // 2. Reset UI
        ResetUIState();
        isProcessingInput = false; 

        // 3. Restart the UI coroutine to show the new first question
        StartCoroutine(InitializeUI());
    }

    private void InitializeButtonListeners()
    {
        for (int index = 0; index < optionButtons.Length; index++)
        {
            int capturedIndex = index;
            if (optionButtons[index] != null)
                optionButtons[index].onClick.AddListener(() => OnOptionClicked(capturedIndex));
        }
    }

    IEnumerator InitializeUI()
    {
        while (quizManager == null || quizManager.GetCurrentQuestion() == null) 
            yield return null;

        if (progressBar != null)
        {
            progressBar.maxValue = quizManager.totalQuestionsThisRound;
            progressBar.value = 0;
        }
        
        UpdateQuestionUI();
        UpdateProgressBar();
    }

    public void OnOptionClicked(int index)
    {
        if (isProcessingInput) return;
        StartCoroutine(ProcessAnswerSequence(index));
    }

    IEnumerator ProcessAnswerSequence(int index)
    {
        isProcessingInput = true; 
        
        Button clickedButton = optionButtons[index];
        if (clickedButton != null)
            clickedButton.GetComponent<ButtonAnimation>()?.buttonClicked();

        DimNonSelectedButtons(index);

        QuestionData currentQuestionData = quizManager.GetCurrentQuestion();
        bool isAnswerCorrect = (index == currentQuestionData.correctAnswerIndex);
        
        SetButtonFeedbackSprite(index, isAnswerCorrect);
        quizManager.ProcessAnswer(isAnswerCorrect);
        UpdateProgressBar();

        yield return new WaitForSeconds(delayBetweenQuestions);

        ResetButtonVisuals();
        
        if (quizManager.GetCurrentQuestion() != null)
        {
            UpdateQuestionUI();
            isProcessingInput = false; 
        }
        else
        {
            quizManager.EvaluateRound(); 
        }
    }

    public void ShowEndScreen(RoundResult result)
    {
        if (gameplayCanvas != null) gameplayCanvas.SetActive(false);
        bool isWin = (result == RoundResult.Win);
        if (winPanel != null) winPanel.SetActive(isWin);
        if (failPanel != null) failPanel.SetActive(!isWin);
    }

    private void UpdateProgressBar()
    {
        if (progressBar != null) progressBar.value = quizManager.correctAnswers;
        if (progressText != null) progressText.text = $"{quizManager.correctAnswers} / {quizManager.totalQuestionsThisRound}";
    }

    private void DimNonSelectedButtons(int selectedButtonIndex)
    {
        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (optionButtons[i] == null) continue;
            Image buttonImage = optionButtons[i].GetComponent<Image>();
            Color targetColor = buttonImage.color;
            targetColor.a = (i == selectedButtonIndex) ? 1.0f : 0.3f;
            buttonImage.color = targetColor;
        }
    }

    private void ResetButtonVisuals()
    {
        foreach (Button currentButton in optionButtons)
        {
            if (currentButton == null) continue;
            Image buttonImage = currentButton.GetComponent<Image>();
            buttonImage.color = new Color(1, 1, 1, 1);
            buttonImage.sprite = defaultSprite;
        }
    }

    private void SetButtonFeedbackSprite(int index, bool isCorrect)
    {
        if (index >= 0 && index < optionButtons.Length && optionButtons[index] != null)
        {
            optionButtons[index].GetComponent<Image>().sprite = isCorrect ? correctSprite : wrongSprite;
        }
    }

    private void UpdateQuestionUI()
    {
        QuestionData questionData = quizManager.GetCurrentQuestion();
        if (questionData != null)
        {
            questionText.text = questionData.questionText;
            for (int i = 0; i < optionButtonTexts.Length; i++)
            {
                if (i < questionData.options.Length)
                    optionButtonTexts[i].text = questionData.options[i];
            }
        }
    }
}