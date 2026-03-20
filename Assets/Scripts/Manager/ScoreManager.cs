using System;
using UnityEngine;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    [SerializeField] private PlayerStatProvider _playerStatProvider;
    private int _runScore;
    private int _totalScore;
    public int RunScore => _runScore;
    public int TotalScore => _totalScore;

    private bool _canScore = false;
    private bool _rewardGiven = false;
    
    public event Action<int> OnRunScoreChanged;
    public event Action<int> OnTotalScoreChanged;

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
                _canScore = false;
                break;
            case GameState.InGame:
                _canScore = true;
                break;
            case GameState.GameOver:
                _canScore = false;
                if (!_rewardGiven)
                {
                    AddToTotalScore(_runScore);
                    _rewardGiven = true;
                }
                break;
        }
    }

    public void AddScore(int amount)
    {
        if (!_canScore) { return; }
        if (_playerStatProvider == null) { return; }
       
        _runScore += Mathf.RoundToInt(amount * _playerStatProvider.ScoreMultiplier);
        OnRunScoreChanged?.Invoke(_runScore);
    }

    public void ResetRunScore()
    {
        _runScore = 0;
        OnRunScoreChanged?.Invoke(_runScore);
    }

    public void AddToTotalScore(int amount)
    {
        _totalScore += amount;
        OnTotalScoreChanged?.Invoke(_totalScore);
    }

    public bool TrySpendTotalScore(int cost)
    {
        if (cost <= 0) { return false; }
        if (cost > _totalScore) { return false; }

        _totalScore -= cost;
        OnTotalScoreChanged?.Invoke(_totalScore);
        return true;
    }
}
