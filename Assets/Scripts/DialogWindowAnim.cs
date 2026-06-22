using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogWindowAnim : MonoBehaviour
{

    [SerializeField] Transform dialogWindow;
    [SerializeField] CanvasGroup dimmer;
    [SerializeField] float dimmerSpeed;

    private void OnEnable() 
    {

        dimmer.alpha = 0;
        dimmer.LeanAlpha(1, dimmerSpeed);

        dialogWindow.localPosition = new Vector2(0, -Screen.height);
        dialogWindow.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;
    
    }

    public void closeDialogWindow() 
    {
        dimmer.LeanAlpha(0, dimmerSpeed);
        dialogWindow.LeanMoveLocalY(-Screen.height, 0.5f).setOnComplete(onComplete);
    
    
    }

    void onComplete() 
    {

        gameObject.SetActive(false);
    
    }


}
