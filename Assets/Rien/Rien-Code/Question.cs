using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionText;
    public string[] answers; // Danh sách các lựa chọn
    public int correctAnswerIndex; // Vị trí đáp án đúng (0, 1, 2, 3...)
}