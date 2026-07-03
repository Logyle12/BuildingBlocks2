using UnityEngine;
using UnityEngine.UI;

public enum sceneDirection
{ 
    LoadNextScene, // (You can probably ignore this one now that we use names)
    LoadPreviousScene,
    LoadStartingScene,
    LoadBySceneName
}

public class SceneControl : MonoBehaviour
{
    private SceneNavigator sceneNavigator; // Renamed
    private string sceneName;
    public sceneDirection sceneDirection;
    private Button thisButton;
   
    void Start()
    {
        // Looks for the new SceneNavigator component
        sceneNavigator = GameObject.FindWithTag("SceneManager").GetComponent<SceneNavigator>();
        thisButton = GetComponent<Button>();

        if (sceneDirection == sceneDirection.LoadPreviousScene)
        {
            thisButton.onClick.AddListener(sceneNavigator.LoadPreviousScene);
        }
        else if (sceneDirection == sceneDirection.LoadBySceneName) 
        {
            sceneName = thisButton.tag;
            thisButton.onClick.AddListener(delegate {sceneNavigator.LoadSceneByName(sceneName);});
        }
    }
}