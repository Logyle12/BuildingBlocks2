using UnityEngine;

public class StageButton : MonoBehaviour
{
    private string myCategory;
    private int myStageIndex;
    public string quizSceneName = "QuizScene"; 

    public void SetupButtonData(string category, int index)
    {
        myCategory = category;
        myStageIndex = index;
    }

    public void OnStageClicked()
    {
        // Write the Context
        PlayerPrefs.SetString("CurrentCategory", myCategory);
        PlayerPrefs.SetInt("CurrentStage", myStageIndex);
        PlayerPrefs.Save();

        // Use the Navigator
        SceneNavigator navigator = GameObject.FindWithTag("SceneManager").GetComponent<SceneNavigator>();
        navigator.LoadSceneByName(quizSceneName);
    }
}