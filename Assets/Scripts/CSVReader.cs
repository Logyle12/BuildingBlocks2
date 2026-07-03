using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public static class CSVReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";

    public static List<QuestionData> ReadCSV(TextAsset csvFile)
    {
        List<QuestionData> questionList = new List<QuestionData>();

        if (csvFile == null)
        {
            Debug.LogError("CSV File is null!");
            return questionList;
        }

        string[] lines = Regex.Split(csvFile.text, LINE_SPLIT_RE);

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] values = Regex.Split(lines[i], SPLIT_RE);

            if (values.Length < 8) continue;

            QuestionData data = new QuestionData
            {
                id = int.Parse(CleanString(values[0])),
                questionType = CleanString(values[1]),
                questionText = CleanString(values[2]),
                options = new string[] 
                {
                    CleanString(values[3]),
                    CleanString(values[4]),
                    CleanString(values[5]),
                    CleanString(values[6])
                }
            };

            if (int.TryParse(CleanString(values[7]), out int answerNum))
            {
                data.correctAnswerIndex = answerNum - 1; 
            }

            questionList.Add(data);
        }

        return questionList;
    }

    private static string CleanString(string input)
    {
        input = input.Trim();
        if (input.StartsWith("\"") && input.EndsWith("\""))
        {
            input = input.Substring(1, input.Length - 2);
        }
        return input.Replace("\"\"", "\"");
    }
}