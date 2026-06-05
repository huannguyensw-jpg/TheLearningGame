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

    public void QuitGame()
    {
        Debug.Log("Game is exiting...");
        Application.Quit();
    }
}