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

<<<<<<< Updated upstream
    void LoadQuestion()
=======
    private int score = 0;              // Biến lưu điểm
    private int currentlevel = 0;       // Câu hỏi hiện tại
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;

    void Start()
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        if (index == questions[currentQuestionIndex].correctAnswerIndex)
=======
        audioSource.PlayOneShot(correctSound);
        score++; // Cộng 1 điểm
        UpdateScoreDisplay(); // Cập nhật chữ hiển thị

        if (currentlevel + 1 < Levels.Length)
>>>>>>> Stashed changes
        {
            score++;
            Debug.Log("Đúng!");
        }
        else { Debug.Log("Sai!"); }

<<<<<<< Updated upstream
        currentQuestionIndex++;
        LoadQuestion();
=======
    // Hàm gọi khi bấm vào đáp án SAI
    public void wrongAnswer()
    {
        audioSource.PlayOneShot(wrongSound);
        if (wrongAnswerPanel != null)
        {
            wrongAnswerPanel.SetActive(true);
            if (gameOverScoreText != null)
                gameOverScoreText.text = "Tổng điểm của bạn: " + score + "/10";
        }
    }

    // Hàm gọi khi bấm nút "Thử lại" trên Panel sai
    public void ResetGame()
    {
        score = 0; // Reset điểm về 0
        currentlevel = 0; // Reset về câu đầu tiên
        UpdateScoreDisplay();

        if (wrongAnswerPanel != null) 
            wrongAnswerPanel.SetActive(false);

        // Duyệt qua danh sách Levels để tắt hết, chỉ bật câu 0
        for (int i = 0; i < Levels.Length; i++)
        {
            if (Levels[i] != null)
                Levels[i].SetActive(i == 0);
        }
    }

    // Hàm cập nhật nội dung chữ TextMeshPro
    void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score + "/10";
        }
>>>>>>> Stashed changes
    }
}