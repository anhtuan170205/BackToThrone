using System;
using UnityEngine;

public class StaminaManager : SingletonMonoBehaviour<StaminaManager>
{
    [SerializeField] private PlayerStats _playerStats;
    private float _initialStamina => _playerStats.Stamina;
    private float _staminaDrainRate => _playerStats.StaminaDrainRate;
    private float _currentStamina;
    public float CurrentStamina => _currentStamina;

    public event Action<float> OnStaminaChanged;
    public event Action OnStaminaDepleted;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (_currentStamina > 0)
        {
            AddStamina(-_staminaDrainRate * Time.deltaTime);
        }
        else
        {
            _currentStamina = 0;
            OnStaminaDepleted?.Invoke();
        }
    }  

    public void AddStamina(float amount)
    {
        if (GameManager.Instance.CurrentGameState != GameState.InGame) return;
        _currentStamina += amount;
        OnStaminaChanged?.Invoke(_currentStamina);
    } 

    public void ResetStamina()
    {
        _currentStamina = _initialStamina;
        OnStaminaChanged?.Invoke(_currentStamina);
    }
}
