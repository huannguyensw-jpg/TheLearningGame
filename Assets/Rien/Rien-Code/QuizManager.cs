using UnityEngine;
using TMPro; // Thư viện bắt buộc để dùng TextMeshPro

public class QuizManager : MonoBehaviour
{
    [Header("Level Management")]
    public GameObject[] Levels;         // Danh sách các Panel chứa từng câu hỏi
    public GameObject wrongAnswerPanel; // Panel hiện lên khi trả lời sai

    [Header("Score Settings")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverScoreText;

    private int score = 0;              // Biến lưu điểm
    private int currentlevel = 0;       // Câu hỏi hiện tại

    void Start()
    {
        ResetGame();
    }

    // Hàm gọi khi bấm vào đáp án ĐÚNG
    public void correctAnswer()
    {
        score++; // Cộng 1 điểm
        UpdateScoreDisplay(); // Cập nhật chữ hiển thị

        if (currentlevel + 1 < Levels.Length)
        {
            // Tắt câu hiện tại, bật câu kế tiếp
            Levels[currentlevel].SetActive(false);
            currentlevel++;
            Levels[currentlevel].SetActive(true);
        }
        else
        {
            // Nếu đã vượt qua câu cuối (Câu 10)
            Debug.Log("Hoàn thành xuất sắc 10 câu hỏi!");
            // Bạn có thể bật một Panel Victory ở đây nếu muốn
        }
    }

    // Hàm gọi khi bấm vào đáp án SAI
    public void wrongAnswer()
    {
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
    }
}