using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelScript : MonoBehaviour
{
    public GameObject toggleCreditsText;
    public GameObject toggleCScoreText;
    public GameObject levelCanvas;
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        if (toggleCreditsText != null)
            toggleCreditsText.SetActive(false);

        if (toggleCScoreText != null)
            toggleCScoreText.SetActive(true);
    }
    public void LoadMainMenu()
    {
        DestroyLevelCanvas();
        SceneManager.LoadScene(0);
        UnlockCursor();
    }
    public void Level1()
    {
        DestroyLevelCanvas();
        SceneManager.LoadScene(1,LoadSceneMode.Single);
        LockCursor();
    }
    public void Level2()
    {
        DestroyLevelCanvas();
        SceneManager.LoadScene(2,LoadSceneMode.Single);
        LockCursor();
    }
    public void Level3()
    {
        DestroyLevelCanvas(); 
        SceneManager.LoadScene(3,LoadSceneMode.Single);
        LockCursor();
    }

    public void Level4()
    {
        DestroyLevelCanvas();
        SceneManager.LoadScene(4, LoadSceneMode.Single);
        LockCursor();
    }
    private void DestroyLevelCanvas()
    {
        if (levelCanvas != null)
        {
            Destroy(levelCanvas);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ToggleCredits()
    {
        if (toggleCreditsText != null && toggleCScoreText != null)
        {
            bool isCreditsActive = !toggleCreditsText.activeSelf;
        
            toggleCreditsText.SetActive(isCreditsActive);
            toggleCScoreText.SetActive(!isCreditsActive);
        }
    }
    private void LockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
}
