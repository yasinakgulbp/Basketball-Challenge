using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public TMP_Text leaderboardText;
    public TMP_Text yourGoalCountText;
    private int highScore;
    private float newScore;

    void Start()
    {
        // PlayerPrefs'ten y�ksek skoru al
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateLeaderboardText();
    }

    // Lider tahtas�n� g�ncelle
    void UpdateLeaderboardText()
    {
        leaderboardText.text = "Best Scrore: " + highScore.ToString();
        yourGoalCountText.text = "Your Scrore: " + newScore.ToString();
    }

    // Yeni bir skor geldi�inde kontrol et ve gerekirse g�ncelle
    public void CheckAndUpdateScore()
    {
        if (newScore > highScore)
        {
            AudioManager2.instance.PlaySoundEffect(4);
            highScore = Mathf.RoundToInt(newScore);
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            UpdateLeaderboardText();
            Debug.Log("Leaderkodu -  highScore : " + highScore);
        }
        else
        {
            UpdateLeaderboardText();
            AudioManager2.instance.PlaySoundEffect(3);
        }
    }

    public void GoalUptade(float newGoal)
    {
        newScore = newGoal;
        Debug.Log("Leaderkodu -  newScore : " + newScore);
    }
}
