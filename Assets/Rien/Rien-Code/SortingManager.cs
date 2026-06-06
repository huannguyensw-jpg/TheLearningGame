using UnityEngine;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class SortingLevel
{
    public string levelName;          // Tên hiển thị gợi nhớ (Ví dụ: Câu 1, Câu 2)
    public GameObject levelContainer; // Object cha chứa tất cả vật phẩm của câu này
    [TextArea(2, 3)]
    public string conditionText;      // Yêu cầu đề bài riêng của câu này
    public float timeLimit = 30f;     // Thời gian giới hạn riêng cho câu này
}

public class SortingManager : MonoBehaviour
{
    [Header("UI System")]
    public Canvas mainCanvas;
    public RectTransform basketRect;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI conditionText;
    public TextMeshProUGUI feedbackText;
    public GameObject gameOverPanel;
    public GameObject victoryPanel; // Bảng thông báo khi thắng TẤT CẢ các câu

    [Header("Multi-Level Settings")]
    public List<SortingLevel> levels; // Danh sách các câu hỏi khác nhau
    private int currentLevelIndex = 0;

    private float currentTimer;
    private int score = 0;
    private bool isGameActive = true;

    private int totalCorrectItems = 0;
    private int sortedCorrectItems = 0;

    void Start()
    {
        feedbackText.gameObject.SetActive(false);
        gameOverPanel.SetActive(false);
        if (victoryPanel != null) victoryPanel.SetActive(false);
        
        currentLevelIndex = 0;
        LoadLevel(currentLevelIndex);
    }

    void Update()
    {
        if (!isGameActive) return;

        if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
            timerText.text = "Thời gian: " + Mathf.CeilToInt(currentTimer) + "s";
        }
        else
        {
            TimeOutLoss();
        }
    }

    // Hàm nạp dữ liệu cho từng Câu hỏi cụ thể
    void LoadLevel(int index)
    {
        if (index >= levels.Count)
        {
            WinAllGame();
            return;
        }

        // 1. Tắt tất cả các câu, chỉ bật duy nhất câu hiện tại
        for (int i = 0; i < levels.Count; i++)
        {
            if (levels[i].levelContainer != null)
                levels[i].levelContainer.SetActive(i == index);
        }

        // 2. Thiết lập thông tin chữ và thời gian của câu đó
        conditionText.text = levels[index].conditionText;
        currentTimer = levels[index].timeLimit;

        // 3. Quét và đếm số vật phẩm ĐÚNG trong câu này
        sortedCorrectItems = 0;
        totalCorrectItems = 0;
        
        // Chỉ quét các script DraggableItem nằm bên trong levelContainer của câu hiện tại
        DraggableItem[] itemsInLevel = levels[index].levelContainer.GetComponentsInChildren<DraggableItem>(true);
        foreach (var item in itemsInLevel)
        {
            if (item.isCorrectItem) totalCorrectItems++;
        }

        isGameActive = true;
    }

    public void CheckDroppedItem(DraggableItem item)
    {
        if (!isGameActive) return;

        if (item.isCorrectItem)
        {
            item.gameObject.SetActive(false); // Biến mất đồ vật đúng
            sortedCorrectItems++;
            score += 10;
            ShowFeedback("Chính xác! +10", Color.green);

            // Nếu đã gom đủ tất cả đồ đúng của CÂU NÀY
            if (sortedCorrectItems >= totalCorrectItems)
            {
                NextLevel();
            }
        }
        else
        {
            ShowFeedback("Wrong!", Color.red);
            item.ReturnToOldPositionWithPenalty();
        }
    }

    void NextLevel()
    {
        isGameActive = false;
        currentLevelIndex++; // Tăng số thứ tự câu hỏi lên 1
        
        if (currentLevelIndex < levels.Count)
        {
            ShowFeedback("Qua màn! Chuẩn bị câu kế tiếp...", Color.cyan);
            // Đợi 1.5 giây rồi tự động nạp câu tiếp theo để người chơi kịp nhìn hiệu ứng
            Invoke(nameof(StartNextLevelDelayed), 1.5f);
        }
        else
        {
            WinAllGame();
        }
    }

    void StartNextLevelDelayed()
    {
        LoadLevel(currentLevelIndex);
    }

    private void ShowFeedback(string message, Color color)
    {
        feedbackText.text = message;
        feedbackText.color = color;
        feedbackText.gameObject.SetActive(true);
        CancelInvoke(nameof(HideFeedback)); 
        Invoke(nameof(HideFeedback), 1.2f);
    }

    private void HideFeedback() { feedbackText.gameObject.SetActive(false); }

    void WinAllGame()
    {
        isGameActive = false;
        if (victoryPanel != null) victoryPanel.SetActive(true);
        conditionText.text = "CHÚC MỪNG! BẠN ĐÃ CHIẾN THẮNG TẤT CẢ THỬ THÁCH.";
    }

    void TimeOutLoss()
    {
        isGameActive = false;
        score = 0; // Reset điểm về 0 theo đề bài
        gameOverPanel.SetActive(true);
    }

    // Nút "Thử lại" khi thua sẽ reset hoàn toàn về Câu 1
    public void RestartMiniGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}