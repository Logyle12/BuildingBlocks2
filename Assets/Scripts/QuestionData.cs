[System.Serializable]
public class QuestionData
{
    public string questionType;
    public string questionText;
    public string[] options = new string[4];
    public int correctAnswerIndex;
    
    // Default weight is 1.0. We reduce this if they get it wrong.
    public float weight = 1.0f; 
}