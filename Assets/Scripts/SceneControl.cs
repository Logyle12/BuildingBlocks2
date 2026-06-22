using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum sceneDirection
{ 
    LoadNextScene,
    LoadPreviousScene,
    LoadStartingScene,
    LoadBySceneName


}

public class SceneControl : MonoBehaviour
{
    private SceneLoader sceneControl;

    private string sceneName;

    private QuizManagement buttonQuizManager;

    public sceneDirection sceneDirection;

    private Button thisButton;
   


    void Start()
    {
        sceneControl = GameObject.FindWithTag("SceneManager").GetComponent<SceneLoader>();
        thisButton = GetComponent<Button>();


        if (sceneDirection == sceneDirection.LoadNextScene)
        {

            thisButton.onClick.AddListener(sceneControl.LoadNextScene);

        }

        else if (sceneDirection == sceneDirection.LoadPreviousScene)
        {

            thisButton.onClick.AddListener(sceneControl.LoadPreviousScene);


        }

        else if (sceneDirection == sceneDirection.LoadBySceneName) 
        {

            sceneName = thisButton.tag;

            thisButton.onClick.AddListener(delegate {sceneControl.LoadSceneByName(sceneName);});
            
        
        }

    }


}
