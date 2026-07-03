using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

// Manages time-delayed transitions between different game scenes.
public class SceneLoader : MonoBehaviour
{
    // Pacing delay for scene changes, editable in the inspector.
    [SerializeField]
    private float delayDuration = 1f;

    // Stores the build index of the current active scene.
    private int currentIndex;

    private Stack<int> sceneStack = new();

    private void Awake()
    {
      DontDestroyOnLoad(gameObject);  
    }

    // Initializes scene tracking when the script wakes up.
    private void Start()
    {
        // Queries and stores the active scene's index.
        currentIndex = SceneManager.GetActiveScene().buildIndex;

        sceneStack.Push(currentIndex);

        Debug.Log("Scene Indexes: " + string.Join(", ", sceneStack.ToArray()));
        Debug.Log($"Scene Total: {sceneStack.ToArray().Length}");
    }

    // Central pipeline handling the pause delay and loading route.
    private IEnumerator MasterLoadRoutine(string sceneName, int sceneIndex)
    {
        // Pauses code execution for the duration of the delay.
        yield return new WaitForSeconds(delayDuration);

        // Checks if a valid scene text name was provided.
        if (!string.IsNullOrEmpty(sceneName))
        {
            // Loads the target scene asset using its name.
            SceneManager.LoadScene(sceneName);
        }
        // Fallback route if loading by index number instead.
        else
        {
            // Loads the target scene asset using its build index.
            SceneManager.LoadScene(sceneIndex);
        }
    }

    // Public entry point to trigger named transitions from UI buttons.
    public void LoadSceneByName(string sceneName)
    {
        // Starts the delay loop while ignoring the numeric index parameter.
        StartCoroutine(MasterLoadRoutine(sceneName, -1));
    }

    // Public entry point to move forward along the scene timeline.
    public void LoadNextScene()
    {
        // Calculates the next sequential scene position.
        int nextIndex = currentIndex + 1;

        // Safety guardrail ensuring the next index exists in build settings.
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Forwards the verified index number to the master delay routine.
            StartCoroutine(MasterLoadRoutine("", nextIndex));
        }
        // Executed only when trying to advance past the final scene.
        else
        {
            // Logs a diagnostic warning to the editor console.
            Debug.LogWarning("No next scene available.");
        }
    }

    // Public entry point to navigate backward along the scene timeline.
    public void LoadPreviousScene()
    {
        // Calculates the preceding sequential scene position.
        int previousIndex = currentIndex - 1;

        // Safety guardrail preventing index numbers from dropping below zero.
        if (previousIndex >= 0)
        {
            // Forwards the verified historical index to the master delay routine.
            StartCoroutine(MasterLoadRoutine("", previousIndex));
        }
    }

}