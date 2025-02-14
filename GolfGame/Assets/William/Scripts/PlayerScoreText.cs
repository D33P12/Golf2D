using TMPro;
using UnityEngine;

public class PlayerScoreText : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI playerScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        playerScoreText = GetComponent<TMPro.TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        playerScoreText.text = ScoreManager.Instance.GetCurrentScore() + "";
    }

}
