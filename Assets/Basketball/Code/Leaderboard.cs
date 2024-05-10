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
        // PlayerPrefs'ten yüksek skoru al
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateLeaderboardText();
    }

    // Lider tahtasýný güncelle
    void UpdateLeaderboardText()
    {
        leaderboardText.text = "Best Scrore: " + highScore.ToString();
        yourGoalCountText.text = "Your Scrore: " + newScore.ToString();
    }

    // Yeni bir skor geldiðinde kontrol et ve gerekirse güncelle
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
