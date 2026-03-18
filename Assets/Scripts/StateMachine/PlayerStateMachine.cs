using System;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public PlayerStats PlayerStats { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CollisionHandler CollisionHandler { get; private set; }

    private void Start()
    {
        SwitchState(new PlayerMoveState(this));
        CollisionHandler.OnCollideWithHazard += HandleCollideWithHazardAndObstacle;
        CollisionHandler.OnCollideWithObstacle += HandleCollideWithHazardAndObstacle;
        GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
        InputReader.JumpEvent += HandleJump;
    }

    private void HandleCollideWithHazardAndObstacle()
    {
        SwitchState(new PlayerStumbleState(this));
    }

    private void HandleGameStateChanged(GameState newState)
    {
        if (newState == GameState.GameOver)
        {
            SwitchState(new PlayerLoseState(this));
        }
    }

    private void HandleJump()
    {
        if (CurrentState is PlayerMoveState)
        {
            SwitchState(new PlayerJumpState(this));
        }
    }

}
