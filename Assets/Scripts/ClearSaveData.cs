using UnityEngine;

public class ClearSaveData : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("If checked, all PlayerPrefs will be deleted when the game starts.")]
    public bool clearDataOnStart = false;

    void Start()
    {
        if (clearDataOnStart)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            
            Debug.Log("<color=red><b>[ClearSaveData]</b> PlayerPrefs have been cleared!</color>");
        }
        else
        {
            Debug.Log("[ClearSaveData] ClearDataOnStart is unchecked. No data was deleted.");
        }
    }
}