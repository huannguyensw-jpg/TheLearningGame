using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuizManager : MonoBehaviour
{
    public List<Question> questions; // Kéo danh sách câu hỏi vào đây
    public Text questionTextUI;
    public Button[] answerButtons; // Gán các nút trả lời vào đây
    
    private int currentQuestionIndex = 0;
    private int score = 0;

    void Start() { LoadQuestion(); }

    void LoadQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            Question q = questions[currentQuestionIndex];
            questionTextUI.text = q.questionText;

            for (int i = 0; i < answerButtons.Length; i++)
            {
                int index = i; // Lưu lại giá trị i để dùng trong callback
                answerButtons[i].GetComponentInChildren<Text>().text = q.answers[i];
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => CheckAnswer(index));
            }
        }
        else { Debug.Log("Game Over! Điểm của bạn: " + score); }
    }

    public void CheckAnswer(int index)
    {
        if (index == questions[currentQuestionIndex].correctAnswerIndex)
        {
            score++;
            Debug.Log("Đúng!");
        }
        else { Debug.Log("Sai!"); }

        currentQuestionIndex++;
        LoadQuestion();
    }
}