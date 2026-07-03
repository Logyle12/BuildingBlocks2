using UnityEngine;
using UnityEngine.SceneManagement; // <-- Added this to read the scene name

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
        // 1. Save the Quiz Data (e.g., "Spelling", "Grammar")
        PlayerPrefs.SetString("CurrentCategoryPlaying", myCategory);
        PlayerPrefs.SetInt("CurrentStagePlaying", myStageIndex);
        
        // 2. DYNAMIC RETURN: Memorize the exact scene we are leaving (e.g., "EnglishLevels")
        PlayerPrefs.SetString("ReturnSceneName", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        // 3. Load the Quiz
        SceneNavigator navigator = GameObject.FindWithTag("SceneManager").GetComponent<SceneNavigator>();
        navigator.LoadSceneByName(quizSceneName);
    }
}