using UnityEngine;
using UnityEngine.UI;
using TMPro; // Required to read the Screen Label text

public class LevelManager : MonoBehaviour
{
    [Header("Category Menus")]
    [Tooltip("Drag your SpellingMenu, PrefixMenu, etc. directly here in order")]
    public GameObject[] categoryMenus;

    void Start()
    {
        ResetAllStages();
    }

    private void ResetAllStages()
    {
        foreach (GameObject menuRoot in categoryMenus)
        {
            if (menuRoot == null) continue;

            Transform groupStage = menuRoot.transform.Find("Group_Stage");
            if (groupStage != null)
            {
                for (int i = 0; i < groupStage.childCount; i++)
                {
                    ResetSingleStage(groupStage.GetChild(i));
                }
            }
        }
    }

    private void ResetSingleStage(Transform stage)
    {
        Transform focusTransform = stage.Find("Focus");
        if (focusTransform != null) focusTransform.gameObject.SetActive(false);

        Transform starContainer = stage.Find("Star");
        if (starContainer != null)
        {
            for (int i = 0; i < starContainer.childCount; i++)
            {
                Image starIcon = starContainer.GetChild(i).GetComponent<Image>();
                if (starIcon != null) starIcon.color = Color.black;
            }
        }
    }

    /// <summary>
    /// Grabs the exact category name from the ScreenLabel text in the UI
    /// </summary>
    private string GetCategoryName(GameObject menuRoot)
    {
        // EXACT match for "ScreenLabel" as you specified
        Transform label = menuRoot.transform.Find("ScreenLabel");

        if (label != null)
        {
            TextMeshProUGUI tmpText = label.GetComponent<TextMeshProUGUI>();
            if (tmpText != null) 
            {
                return tmpText.text;
            }
        }

        // Fallback just in case something is named differently
        Debug.LogWarning($"Could not read ScreenLabel on {menuRoot.name}");
        return "UnknownCategory";
    }

    public void LoadCategoryProgress(int categoryIndex)
    {
        if (categoryIndex < 0 || categoryIndex >= categoryMenus.Length) return;

        GameObject currentMenu = categoryMenus[categoryIndex];
        if (currentMenu == null) return;

        // Automatically fetch the name from your UI Text!
        string categoryName = GetCategoryName(currentMenu);
        Transform groupStage = currentMenu.transform.Find("Group_Stage");
        
        if (groupStage != null)
        {
            for (int i = 0; i < groupStage.childCount; i++)
            {
                Transform stage = groupStage.GetChild(i);

                // Pass the data to the button so it can write the sticky note when clicked
                StageButton btnScript = stage.GetComponent<StageButton>();
                if (btnScript != null) btnScript.SetupButtonData(categoryName, i);

                // Create the unique save key (e.g., "Prefix_Stage_0_Stars")
                string saveKey = categoryName + "_Stage_" + i + "_Stars";
                
                int starsEarned = PlayerPrefs.GetInt(saveKey, 0);
                UpdateStageVisuals(stage, starsEarned);
            }
        }
    }

    private void UpdateStageVisuals(Transform stage, int starsEarned)
    {
        Transform starContainer = stage.Find("Star");
        if (starContainer != null)
        {
            for (int i = 0; i < starContainer.childCount; i++)
            {
                Image starIcon = starContainer.GetChild(i).GetComponent<Image>();
                if (starIcon != null)
                {
                    starIcon.color = (i < starsEarned) ? Color.white : Color.black;
                }
            }

            Transform focusTransform = stage.Find("Focus");
            if (focusTransform != null)
            {
                bool hasAllStars = (starsEarned >= starContainer.childCount);
                focusTransform.gameObject.SetActive(hasAllStars);
            }
        }
    }
}