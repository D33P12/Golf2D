using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame()
    {
        SoundManager.Instance.PlayAudio(SoundManager.AudioType.ButtonClick);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        SoundManager.Instance.PlayAudio(SoundManager.AudioType.ButtonClick);
        Application.Quit();
    }
     
}
