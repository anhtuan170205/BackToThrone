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
    }

    public void AddScore(int amount)
    {
        if (GameManager.Instance.CurrentGameState == GameState.GameOver) { return; }
        _score += amount;
        OnScoreChanged?.Invoke(_score);
    }

    public void ResetScore()
    {
        _score = 0;
        OnScoreChanged?.Invoke(_score);
    }

}
