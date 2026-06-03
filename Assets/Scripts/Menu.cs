using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [Header("Названия сцен")]
    public string gameSceneName = "schena 1";
    public string settingsSceneName = "settings";
    
    [Header("Панели")]
    public GameObject settingsPanel;
    public GameObject confirmPanel;
    public Text confirmText;
    
    [Header("Ссылки на кнопки")]
    public Button continueButton;
    
    void Start()
    {
        UpdateContinueButton();
        
        if (confirmPanel != null)
            confirmPanel.SetActive(false);
        
        // Настройка курсора в главном меню
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    void UpdateContinueButton()
    {
        if (continueButton != null)
        {
            // Проверяем наличие сохранения
            bool hasSave = PlayerPrefs.HasKey("GameSaved");
            continueButton.interactable = hasSave;
            
            Debug.Log($"Кнопка продолжения {(hasSave ? "активна" : "неактивна")}");
        }
    }
    
    // ========== КНОПКА "НОВАЯ ИГРА" ==========
    public void StartGame()
    {
        if (PlayerPrefs.HasKey("GameSaved"))
        {
            ShowConfirmPanel("Начать новую игру? Весь текущий прогресс будет потерян!");
        }
        else
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }
    
    public void ConfirmNewGame()
    {
        // Очищаем все сохранения
        PlayerPrefs.DeleteKey("GameSaved");
        PlayerPrefs.DeleteKey("LastScene");
        PlayerPrefs.DeleteKey("PlayerX");
        PlayerPrefs.DeleteKey("PlayerY");
        PlayerPrefs.DeleteKey("PreviousScene");  // Добавляем очистку
        PlayerPrefs.Save();
        
        if (confirmPanel != null)
            confirmPanel.SetActive(false);
        
        SceneManager.LoadScene(gameSceneName);
    }
    
    public void CancelNewGame()
    {
        if (confirmPanel != null)
            confirmPanel.SetActive(false);
    }
    
    // ========== КНОПКА "ПРОДОЛЖИТЬ" ==========
    public void ContinueGame()
    {
        if (PlayerPrefs.HasKey("GameSaved"))
        {
            string lastScene = PlayerPrefs.GetString("LastScene", gameSceneName);
            Debug.Log($"Загрузка сохранённой игры: {lastScene}");
            SceneManager.LoadScene(lastScene);
        }
        else
        {
            Debug.Log("Нет сохранённой игры!");
        }
    }
    
    // ========== КНОПКА "ВЫХОД" ==========
    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    
    // ========== КНОПКА "НАСТРОЙКИ" ==========
    public void OpenSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }
    
    public void CloseSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }
    
    void ShowConfirmPanel(string message)
    {
        if (confirmPanel != null)
        {
            if (confirmText != null)
                confirmText.text = message;
            confirmPanel.SetActive(true);
        }
        else
        {
            #if UNITY_EDITOR
            if (UnityEditor.EditorUtility.DisplayDialog("Подтверждение", message, "Да", "Нет"))
            {
                ConfirmNewGame();
            }
            #endif
        }
    }
}