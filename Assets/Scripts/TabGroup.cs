using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    // Start is called before the first frame update

    public List<TabButton> tabButtons;

    public Sprite tabIdleSprite;
    public Sprite tabHoverSprite;
    public Sprite tabActiveSprite;

    public TabButton selectedTab;
    public List<GameObject> screensToSwap;
    public void addTab(TabButton button)
    {

        if (tabButtons == null) {

            tabButtons = new List<TabButton>();

        }

        tabButtons.Add(button);

    }

    public void onTabEnter(TabButton button)
    {
        resetTabs();

        if (selectedTab == null || button  != selectedTab) 
        {
            button.background.sprite = tabHoverSprite;   
        }

    }
    public void onTabSelected (TabButton button) 
    {
        selectedTab = button;
        resetTabs();
        button.background.sprite = tabActiveSprite;
        
        int tabIndex = button.transform.GetSiblingIndex();

        for (int i = 0; i < screensToSwap.Count; i++) 
        {

            if (i == tabIndex)
            {

                screensToSwap[i].SetActive(true);
            }

            else 
            {

                screensToSwap[i].SetActive(false);
            
            }
        
        }

        
    }
    public void onTabExit (TabButton button) 
    {
        resetTabs();
    }   
    public void resetTabs() 
    {
        foreach (TabButton button in tabButtons) 
        {
            if (selectedTab != null & button == selectedTab) 
            {
                continue;    
            }

            button.background.sprite = tabIdleSprite;
        
        }
    
    }
    
}
