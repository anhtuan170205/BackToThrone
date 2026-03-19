using System;
using UnityEngine;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    private int _score;
    public int Score => _score;

    private bool _canScore = false;
    
    public event Action<int> OnScoreChanged;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.MainMenu:
                ResetScore();
                _canScore = false;
                break;
            case GameState.InGame:
                _canScore = true;
                break;
            case GameState.GameOver:
                _canScore = false;
                break;
        }
    }

    public void AddScore(int amount)
    {
        if (!_canScore) { return; }
        
        _score += amount;
        OnScoreChanged?.Invoke(_score);
    }

    public void ResetScore()
    {
        _score = 0;
        OnScoreChanged?.Invoke(_score);
    }

}
