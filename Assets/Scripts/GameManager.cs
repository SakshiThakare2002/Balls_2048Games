using UnityEngine;
using TMPro;
using Yudiz.StarterKit.Utilities;

public class GameManager : Singleton<GameManager>
{
    private int score = 0;
    private int level = 1;
    private const int scoreToLevelUp = 100;

    public int Score { get { return score; } }
    public int Level { get { return level; } }

    public delegate void ScoreUpdate(int score, int level);
    public event ScoreUpdate OnScoreUpdate;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        ResetScore();
        ResetLevel();
    }

    public void AddScore(int ballValue)
    {
        int scoreIncrement = (int)Mathf.Log(ballValue, 2);
        score += scoreIncrement;

        if (score / scoreToLevelUp >= level)
        {
            level++;
        }

        if (OnScoreUpdate != null)
        {
            Debug.Log($"Event triggered: Score = {score}, Level = {level}");
            OnScoreUpdate.Invoke(score, level);
        }
        else
        {
            Debug.LogWarning("No listeners subscribed to OnScoreUpdate.");
        }
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void ResetLevel()
    {
        level = 1;
    }
}
