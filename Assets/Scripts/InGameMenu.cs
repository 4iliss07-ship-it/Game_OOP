using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOpener : MonoBehaviour
{
    public string menuSceneName = "MenuEsc";
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Сохраняем текущую сцену
            PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();
            
            // Загружаем меню
            SceneManager.LoadScene(menuSceneName);
        }
    }
}