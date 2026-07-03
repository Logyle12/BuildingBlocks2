using UnityEngine;

[CreateAssetMenu(fileName = "GameContext", menuName = "Game/GameContext")]
public class GameContext : ScriptableObject
{
    // These store the session state
    public string currentCategory;
    public int currentStageIndex;

    // Helper to update state
    public void SetContext(string category, int stageIndex)
    {
        currentCategory = category;
        currentStageIndex = stageIndex;
    }
}