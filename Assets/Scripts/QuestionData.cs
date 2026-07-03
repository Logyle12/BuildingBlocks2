[System.Serializable]
public class QuestionData
{
    public int id; // Added this
    public string questionType;
    public string questionText;
    public string[] options = new string[4];
    public int correctAnswerIndex;
    public float weight = 1.0f; 
}