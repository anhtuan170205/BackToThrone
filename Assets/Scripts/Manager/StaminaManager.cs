using System;
using UnityEngine;

public class StaminaManager : SingletonMonoBehaviour<StaminaManager>
{
    [SerializeField] private PlayerStatProvider _playerStatProvider;
    private float _currentStamina;
    public float CurrentStamina => _currentStamina;
    private bool _isDraining = false;

    public event Action<float> OnStaminaChanged;
    public event Action OnStaminaDepleted;

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

    private void Update()
    {
        if (!_isDraining) { return; }

        if (_currentStamina > 0)
        {
            AddStamina(-Time.deltaTime);
        }
        else
        {
            _currentStamina = 0;
            OnStaminaDepleted?.Invoke();
        }
    }  

    private void HandleGameStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.MainMenu:
                ResetStamina();
                StopDraining();
                break;
            case GameState.InGame:
                StartDraining();
                break;
            case GameState.GameOver:
                StopDraining();
                break;
        }
    }

    public void AddStamina(float amount)
    {
        if (GameManager.Instance.CurrentGameState != GameState.InGame) return;

        _currentStamina += amount;
        _currentStamina = Mathf.Clamp(_currentStamina, 0, _playerStatProvider.MaxStamina);
        OnStaminaChanged?.Invoke(_currentStamina);
    } 

    public void ResetStamina()
    {
        _currentStamina = _playerStatProvider.MaxStamina;
        OnStaminaChanged?.Invoke(_currentStamina);
    }   

    public void StartDraining()
    {
        _isDraining = true;
    }

    public void StopDraining()
    {
        _isDraining = false;
    }
}
