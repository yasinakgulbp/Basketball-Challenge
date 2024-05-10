using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public Leaderboard leaderboard;
    public GameObject gameOverUI;
    public TMP_Text timerText;
    public float totalTime = 60f; // Toplam zamaný saniye cinsinden ayarla
    private float currentTime;
    private bool isPaused = false;
    private bool isGameOver = false;

    private void Start()
    {
        currentTime = totalTime;
        UpdateTimerText();
    }

    private void Update()
    {
        if (!isPaused && !isGameOver)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                if (currentTime < 0) // Süre 0'dan küçükse 0 yap
                {
                    currentTime = 0;
                }
                UpdateTimerText();
            }
            else
            {
                GameOver();
            }
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
    }

    private void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        leaderboard.CheckAndUpdateScore();
        // Burada Game Over UI'sýný aktifleþtirebilirsin
        // Örneðin:
        gameOverUI.SetActive(true);
        Debug.Log("oyun bitti Leader board scripti debug kodu.");
    }
}
