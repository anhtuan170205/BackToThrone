using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerBaseStats", menuName = "Scriptable Objects/PlayerBaseStats", order = 1)]
public class PlayerBaseStats : ScriptableObject
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 8f;
    [SerializeField] private float _stamina = 10f;
    [SerializeField] private float _staminaBoostAmount = 5f;
    public float MoveSpeed => _moveSpeed;
    public float JumpForce => _jumpForce;
    public float Stamina => _stamina;
    public float StaminaBoostAmount => _staminaBoostAmount;
}
