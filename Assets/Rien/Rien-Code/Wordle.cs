using UnityEngine;
using TMPro; // Bắt buộc để dùng InputField của TextMeshPro

public class TypingGame : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField inputField;      // Kéo InputField vào đây
    public TextMeshProUGUI questionText;   // Text hiển thị từ cần nhập
    public GameObject wrongPanel;          // Panel báo sai

    [Header("Game Data")]
    public string[] targetWords;           // Danh sách từ cần nhập
    private int currentIndex = 0;

    void Start()
    {
        SetupLevel();
        // Lắng nghe sự kiện khi người dùng nhấn Enter
        inputField.onSubmit.AddListener(CheckInput);
    }

    void SetupLevel()
    {
        if (currentIndex < targetWords.Length)
        {
            questionText.text = "Nhập từ: " + targetWords[currentIndex];
            inputField.text = ""; // Xóa trắng ô input
            inputField.ActivateInputField(); // Tự động focus vào ô nhập
        }
    }

    void CheckInput(string input)
    {
        if (input.ToLower() == targetWords[currentIndex].ToLower())
        {
            // Nhập đúng
            currentIndex++;
            if (currentIndex < targetWords.Length)
            {
                SetupLevel();
            }
            else
            {
                Debug.Log("Bạn đã thắng!");
            }
        }
        else
        {
            // Nhập sai
            wrongPanel.SetActive(true);
        }
    }

    public void ResetGame()
    {
        currentIndex = 0;
        wrongPanel.SetActive(false);
        SetupLevel();
    }
}