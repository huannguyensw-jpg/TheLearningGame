using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [Header("Settings Panel")]
    public GameObject settingsPanel; // Kéo bảng Settings (UI Panel) vào đây

    public void ChangeScene(string LobbyScene)
    {
        SceneManager.LoadScene(LobbyScene);
    }

    public void ToggleSettings()
    {
        if (settingsPanel != null)
        {
            bool isActive = settingsPanel.activeSelf;
            settingsPanel.SetActive(!isActive);
        }
    }

    // ================= TÍNH NĂNG MỚI THÊM VÀO =================

    /// <summary>
    /// Chức năng Chơi lại: Tự động nạp lại Scene đang chơi hiện tại mà không cần nhập tên.
    /// </summary>
    public void RetryGame()
    {
        // Lấy chỉ số (Build Index) của Scene đang chạy hiện tại
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Tải lại chính nó
        SceneManager.LoadScene(currentSceneIndex);
    }

    /// <summary>
    /// Chức năng trở về Main Menu: Nhập tên Scene Menu của bạn vào ô tham số trong Unity.
    /// </summary>
    public void ReturnToMainMenu(string menuSceneName)
    {
        SceneManager.LoadScene(menuSceneName);
    }

    // ==========================================================

    public void QuitGame()
    {
        Debug.Log("Game is exiting...");
        Application.Quit();
    }
}