using System.Collections;
using UnityEngine;

public class GolfHole : MonoBehaviour
{
    [SerializeField] private GameObject scoreScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        scoreScreen.SetActive(true);
        Time.timeScale = 0f;
    }

}
