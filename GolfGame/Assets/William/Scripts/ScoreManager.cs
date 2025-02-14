using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private int overallScore;
    [SerializeField] private int golfScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void ScorePlusOne()
    {
        golfScore++;
    }

    public int GetCurrentScore()
    {
        return golfScore;
    }

    public int GetOverallScore()
    {
        return overallScore;
    }

    public void ResetCurrentScore()
    {
        golfScore = 0;
    }

    public void ResetOverallScore()
    {
        overallScore = 0;
    }

    public void AddOverallScore()
    {
        overallScore = overallScore + golfScore - LevelInfo.Instance.GetParNumber();
    }
}
