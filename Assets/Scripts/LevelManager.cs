using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private int pageNumber = 0;
    private bool active = false;
    private TextMeshProUGUI pageTitle;
    private List<GameObject> levelScenes = new List<GameObject>();
    [SerializeField] private Transform panelTransform;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;

    void Start()
    {
        pageTitle = transform.GetComponentInChildren<TextMeshProUGUI>();
        prevButton.onClick.AddListener(loadPrevScene);
        nextButton.onClick.AddListener(loadNextScene);

        foreach (Transform levelScene in panelTransform) {

            levelScenes.Add(levelScene.gameObject);
            levelScene.gameObject.SetActive(false);
        
        }

        levelScenes[pageNumber].SetActive(true);
        active = true;

        controlLevelScenes();
    }

    public void loadPrevScene()
    {
        if (pageNumber <= 0 || !active) {

            return;

        } 

        levelScenes[pageNumber].SetActive(false);
        levelScenes[pageNumber -= 1].SetActive(true);
        controlLevelScenes();
    }

    public void loadNextScene()
    {
        if (pageNumber >= levelScenes.Count - 1) {

            return;
        }
        

        levelScenes[pageNumber].SetActive(false);
        levelScenes[pageNumber += 1].SetActive(true);
        controlLevelScenes();

    }


    private void activateArrow() 
    {
        int numberOfLevelScenes = levelScenes.Count;
        prevButton.gameObject.SetActive(pageNumber > 0);
        nextButton.gameObject.SetActive(pageNumber < numberOfLevelScenes - 1);
    
    
    }

    private void controlLevelScenes() 
    {;
        activateArrow();
    
    }

    void Update()
    {
        int numberOfLevelScenes = levelScenes.Count;
        if (numberOfLevelScenes <= 0 || active) {

            return;

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {

            loadPrevScene();

        }

        else if (Input.GetKeyDown(KeyCode.RightArrow)) {

            loadNextScene();
        }

    }
}
