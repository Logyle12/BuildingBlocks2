using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Category Menus")]
    public GameObject[] categoryMenus;

    public void LoadCategoryProgress(int categoryIndex)
    {
        if (categoryIndex < 0 || categoryIndex >= categoryMenus.Length) return;

        GameObject menuRoot = categoryMenus[categoryIndex];
        Transform groupStage = menuRoot.transform.Find("Group_Stage");
        
        if (groupStage != null)
        {
            // Use the menu name as the category key (e.g., "SpellingMenu" -> "Spelling")
            string categoryName = menuRoot.name.Replace("Menu", ""); 

            for (int i = 0; i < groupStage.childCount; i++)
            {
                Transform stage = groupStage.GetChild(i);

                // Pass the data to the button
                StageButton btnScript = stage.GetComponent<StageButton>();
                if (btnScript != null) btnScript.SetupButtonData(categoryName, i);

                // Fetch current stars and update visuals
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
                    // If starsEarned is 1, index 0 is White, rest are Black
                    starIcon.color = (i < starsEarned) ? Color.white : Color.black;
                }
            }

            // Optional: Update a focus/active ring
            Transform focusTransform = stage.Find("Focus");
            if (focusTransform != null)
            {
                bool hasAllStars = (starsEarned >= starContainer.childCount);
                focusTransform.gameObject.SetActive(hasAllStars);
            }
        }
    }
}