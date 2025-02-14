using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreMenu : MonoBehaviour
{
    [NonSerialized] public InputManager inputManager;
    [SerializeField] private int nextSceneNumber;
    private void Awake()
    {
        //Get input manager instance
        inputManager = InputManager.Instance;

        //Player shoot control
        inputManager.GetShoot().performed += x => LoadNextScene(nextSceneNumber);

    }

    private void LoadNextScene(int sceneNumber)
    {
        Time.timeScale = 1f;
        ScoreManager.Instance.AddOverallScore();
        ScoreManager.Instance.ResetCurrentScore();
        SceneManager.LoadScene(sceneNumber);
    }
}
