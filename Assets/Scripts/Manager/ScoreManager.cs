using System;
using UnityEngine;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    private int _score;
    public int Score => _score;
    
    public event Action<int> OnScoreChanged;

    protected override void Awake()
    {
        base.Awake();
        ResetScore();
    }

    public void AddScore(int amount)
    {
        _score += amount;
        OnScoreChanged?.Invoke(_score);
    }

    public void ResetScore()
    {
        _score = 0;
        OnScoreChanged?.Invoke(_score);
    }

}
