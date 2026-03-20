using System;
using System.Diagnostics;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public PlayerStatProvider PlayerStatProvider { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CollisionHandler CollisionHandler { get; private set; }

    private void Start()
    {
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
        switch (newState)
        {
            case GameState.MainMenu:
                SwitchState(new PlayerIdleState(this));
                break;

            case GameState.InGame:
                SwitchState(new PlayerMoveState(this));
                break;

            case GameState.GameOver:
                SwitchState(new PlayerLoseState(this));
                break;
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
