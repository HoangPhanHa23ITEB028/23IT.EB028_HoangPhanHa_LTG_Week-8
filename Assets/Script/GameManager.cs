using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameMenuCanvas; // Kéo thả đối tượng GameMenu vào đây
    public TMP_Text welcomeText;      // Kéo thả chữ Welcome (hoặc Title) vào đây

    void Start()
    {
        // Ẩn bảng GameMenu khi bắt đầu game
        if (gameMenuCanvas != null)
        {
            gameMenuCanvas.SetActive(false);
        }
    }

    // Hàm gọi khi máy bay đâm tường
    public void GameOver()
    {
        if (gameMenuCanvas != null)
        {
            gameMenuCanvas.SetActive(true); // Hiển thị bảng Game Over lên
            if (welcomeText != null)
            {
                welcomeText.text = "Game Over"; // Nội dung hỏi Yes/No
            }
        }
        
        // Đóng băng thời gian trong game lại
        Time.timeScale = 0f; 
    }

    // Gắn hàm này cho nút "Yes" (Chơi lại)
    public void OnYesButtonClicked()
    {
        Time.timeScale = 1f; // Phục hồi lại thời gian
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Load lại Scene từ đầu
    }

    // Gắn hàm này cho nút "No" (Thoát game)
    public void OnNoButtonClicked()
    {
        Time.timeScale = 1f;
        Debug.Log("Đã thoát game!");
        
        // Nếu đang chạy bản Build thực tế (Exe), lệnh này sẽ tắt game
        Application.Quit();

        // Nếu đang test trong Unity Editor, lệnh này sẽ dừng chế độ Play lại
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}