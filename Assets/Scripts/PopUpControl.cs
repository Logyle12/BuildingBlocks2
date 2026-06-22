using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpControl : MonoBehaviour
    
{
    // Start is called before the first frame update

    [SerializeField] GameObject panel;

    public void renderPanel() 
    {

        if (panel != null) { 
            
            panel.SetActive(true); 
        }
        
    
    }

    public void closePanel() 
    {

        if (panel) {

            panel.SetActive(false);
        }
    

    }

    public void renderNewPanel()
    {

        closePanel();
        renderPanel();

    }

}
