using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelSceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int previousSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("Level", previousSceneIndex);

    }
}

