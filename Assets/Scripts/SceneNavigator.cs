using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SceneNavigator : MonoBehaviour
{
    public float delayDuration = 1f;
    private Stack<string> sceneHistory = new Stack<string>();

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("SceneManager").Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        sceneHistory.Push(SceneManager.GetActiveScene().name);
    }

    public void LoadSceneByName(string sceneName)
    {
        StartCoroutine(LoadRoutine(sceneName, true));
    }

    public void LoadPreviousScene()
    {
        if (sceneHistory.Count > 1)
        {
            sceneHistory.Pop(); 
            string previousScene = sceneHistory.Peek(); 
            StartCoroutine(LoadRoutine(previousScene, false));
        }
        else
        {
            Debug.LogWarning("You are at the very beginning, cannot go back!");
        }
    }

    private IEnumerator LoadRoutine(string sceneName, bool isMovingForward)
    {
        yield return new WaitForSeconds(delayDuration);

        if (isMovingForward)
        {
            sceneHistory.Push(sceneName);
        }

        SceneManager.LoadScene(sceneName);
    }
}