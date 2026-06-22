using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelection : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite lockedButtonSprite;

    public Button[] levelButtons;


    void Start()
    {
        levelController();
    }

    private void levelSelect(string levelName) 
    {

        SceneManager.LoadScene(levelName);
    
    }
    private void levelController() 
    { 
      
        int currentLevel = PlayerPrefs.GetInt("currentLevel", 3);

        for (int buttonIndex = 0; buttonIndex < levelButtons.Length; buttonIndex++) 
        {
            if (buttonIndex + 3 > currentLevel)
            {

                
                levelButtons[buttonIndex].interactable = false;
                levelButtons[buttonIndex].image.sprite = lockedButtonSprite;
                levelButtons[buttonIndex].image.color = new Color32(204, 204, 204, 155);
                levelButtons[buttonIndex].GetComponentInChildren<TextMeshProUGUI>().color = new Color32(204, 204, 204, 155);

            }
     
        }
     
    
    }


}
