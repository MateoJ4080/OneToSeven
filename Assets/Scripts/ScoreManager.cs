using UnityEngine;
using System;

public static class ScoreManager
{
    private static int _score = 0;
    public static event Action OnScoreChanged;

    public static int Score
    {
        get { return _score; }
        set
        {
            // Prevent the score from going negative
            if (value < 0)
                _score = 0;
            else
                _score = value;
            OnScoreChanged?.Invoke();
        }
    }

    public static void AddScore(int amount)
    {
        Score += amount;
    }
}