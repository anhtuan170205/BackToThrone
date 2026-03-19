using System;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private GameState _currentGameState;
    public GameState CurrentGameState => _currentGameState;
    public event Action<GameState> OnGameStateChanged;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        ResetGame();
        StaminaManager.Instance.OnStaminaDepleted += HandleGameOver;
    }

    private void OnDestroy()
    {
        StaminaManager.Instance.OnStaminaDepleted -= HandleGameOver;
    }

    public void SetGameState(GameState newState)
    {
        _currentGameState = newState;
        OnGameStateChanged?.Invoke(_currentGameState);
    }

    private void HandleGameOver()
    {
        SetGameState(GameState.GameOver);
    }

    public void ResetGame()
    {   
        SetGameState(GameState.MainMenu);
    }

    public void StartGame()
    {
        SetGameState(GameState.InGame);
    }
}
