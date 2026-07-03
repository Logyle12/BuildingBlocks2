using UnityEngine;
using UnityEngine.SceneManagement;

// This automatically adds a CanvasGroup to your button so we can hide it cleanly
[RequireComponent(typeof(CanvasGroup))]
public class GlobalBackButton : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        // 1. Prevent clones from spawning if we go back to Game Selection
        if (FindObjectsOfType<GlobalBackButton>().Length > 1)
        {
            // Destroy the duplicate's root Canvas
            Destroy(transform.root.gameObject); 
            return;
        }

        canvasGroup = GetComponent<CanvasGroup>();

        // 2. Make the Canvas that holds THIS button immortal
        DontDestroyOnLoad(transform.root.gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 3. Hide in Quiz or Main Menu
        if (scene.name == "QuizScene" || scene.name == "GameMenu")
        {
            canvasGroup.alpha = 0f;             // Make it invisible
            canvasGroup.blocksRaycasts = false; // Make it unclickable
        }
        else
        {
            // 4. Show it everywhere else (GameSelection, EnglishLevels, etc.)
            canvasGroup.alpha = 1f;             // Make it visible
            canvasGroup.blocksRaycasts = true;  // Make it clickable
        }
    }
}