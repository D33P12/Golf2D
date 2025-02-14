using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject PauseMenu;
    public static bool isPaused;

    void Start()
    {
        PauseMenu.SetActive(false);
       
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SoundManager.Instance.PlayAudio(SoundManager.AudioType.ButtonClick);
            if (isPaused)
            {
                
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f; 
        isPaused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void RestartGame()
    {
        
        SoundManager.Instance.PlayAudio(SoundManager.AudioType.ButtonClick);
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void GoToMainMenu()
    {
        SoundManager.Instance.PlayAudio(SoundManager.AudioType.ButtonClick);
        Time.timeScale = 1f; 
        SceneManager.LoadScene(0);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void QuitGame()
    {
        SoundManager.Instance.PlayAudio(SoundManager.AudioType.ButtonClick);
        Application.Quit();
    }
}
