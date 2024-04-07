using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Animator gameOverAnimator;
    public TextMeshProUGUI roundsText;

    private void OnEnable()
    {
        {
            roundsText.text = PlayerStats.Rounds.ToString();
        }
        StartCoroutine(TriggerAnimationNextFrame());
    }

    IEnumerator TriggerAnimationNextFrame()
    {
        yield return null;
        
        if (gameOverAnimator != null)
        {
            gameOverAnimator.SetTrigger("IsOver");
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
