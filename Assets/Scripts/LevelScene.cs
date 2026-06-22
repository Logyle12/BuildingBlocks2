using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScene : MonoBehaviour
{
    [SerializeField] private GameObject[] otherLevelScenes;

    public void OnEnable()
    {
        for (int currentSceneIndex = 0; currentSceneIndex < otherLevelScenes.Length; currentSceneIndex++) otherLevelScenes[currentSceneIndex].SetActive(true);
    }

    public void OnDisable()
    {
        for (int currentSceneIndex = 0; currentSceneIndex < otherLevelScenes.Length; currentSceneIndex++) otherLevelScenes[currentSceneIndex].SetActive(false);
    }
}
