using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _stamina = 10f;
    [SerializeField] private float _staminaDrainRate = 1f;
    [SerializeField] private float _staminaBoostAmount = 5f;
    public float MoveSpeed => _moveSpeed;
    public float Stamina => _stamina;
    public float StaminaDrainRate => _staminaDrainRate;
    public float StaminaBoostAmount => _staminaBoostAmount;
}
