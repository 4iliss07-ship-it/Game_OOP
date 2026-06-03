using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuEscManager : MonoBehaviour
{
    [Header("Привяжите кнопки в инспекторе!")]
    public Button continueButton;
    public Button saveButton;
    public Button exitButton;
    
    private string sceneToReturnTo;
    
    void Start()
    {
        Debug.Log("MenuEscManager запущен");
        
        // Получаем сцену для возврата
        if (PlayerPrefs.HasKey("PreviousScene"))
        {
            sceneToReturnTo = PlayerPrefs.GetString("PreviousScene");
            Debug.Log($"Сцена для возврата: {sceneToReturnTo}");
        }
        
        // Настраиваем курсор
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
        
        // Настраиваем кнопки (привязанные в инспекторе)
        SetupButtons();
    }
    
    void SetupButtons()
    {
        if (continueButton != null)
        {
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(ContinueGame);
            Debug.Log("Кнопка Продолжить настроена");
        }
        else Debug.LogWarning("Continue Button не привязан в инспекторе!");
        
        if (saveButton != null)
        {
            saveButton.onClick.RemoveAllListeners();
            saveButton.onClick.AddListener(SaveGame);
            Debug.Log("Кнопка Сохранить настроена");
        }
        else Debug.LogWarning("Save Button не привязан в инспекторе!");
        
        if (exitButton != null)
        {
            exitButton.onClick.RemoveAllListeners();
            exitButton.onClick.AddListener(ExitToMainMenu);
            Debug.Log("Кнопка Выйти настроена");
        }
        else Debug.LogWarning("Exit Button не привязан в инспекторе!");
    }
    
    public void ContinueGame()
    {
        Debug.Log("Продолжить игру");
        if (!string.IsNullOrEmpty(sceneToReturnTo))
        {
            SceneManager.LoadScene(sceneToReturnTo);
        }
    }
    
    public void SaveGame()
    {
        Debug.Log("Сохранение игры...");
        
        if (!string.IsNullOrEmpty(sceneToReturnTo))
        {
            PlayerPrefs.SetString("LastScene", sceneToReturnTo);
            PlayerPrefs.SetInt("GameSaved", 1);
            PlayerPrefs.Save();
            Debug.Log($"Игра сохранена! Сцена: {sceneToReturnTo}");
        }
    }
    
    public void ExitToMainMenu()
    {
        Debug.Log("Выход в главное меню");
        SceneManager.LoadScene("menu");
    }
}