using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private float delayDuration = 1f; // Serialized field for delay duration with default value of 1 second

    private int currentIndex;

    private void Start()
    {
        // Get the current scene index
        currentIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadSceneByName(string sceneName)
    {
        StartCoroutine(LoadSceneDelayed(sceneName));
    }

    private IEnumerator LoadSceneDelayed(string sceneName)
    {
        yield return new WaitForSeconds(delayDuration);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadNextSceneDelayed());
    }

    private IEnumerator LoadNextSceneDelayed()
    {
        yield return new WaitForSeconds(delayDuration);

        int nextIndex = currentIndex + 1;

        // Check if there is a next scene
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.LogWarning("No next scene available.");
        }
    }

    public void LoadPreviousScene()
    {
        StartCoroutine(LoadPreviousSceneDelayed());
    }

    private IEnumerator LoadPreviousSceneDelayed()
    {
        yield return new WaitForSeconds(delayDuration);

        int previousIndex = currentIndex - 1;

        // Check if there is a previous scene
        if (previousIndex >= 0)
        {
            SceneManager.LoadScene(previousIndex);
        }
        else
        {
            Debug.LogWarning("No previous scene available.");
        }
    }
}


