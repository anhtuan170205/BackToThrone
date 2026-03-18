using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    private readonly int RUN_HASH = Animator.StringToHash("Run");
    public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        StateMachine.Animator.Play(RUN_HASH);
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement() * StateMachine.PlayerStats.MoveSpeed;
        movement += StateMachine.ForceReceiver.Movement;
        Move(movement, deltaTime);
    }

    public override void Exit()
    {
        
    }

    private Vector3 CalculateMovement()
    {
        Vector2 inputMovement = StateMachine.InputReader.MovementValue;
        Vector3 movement = new Vector3(inputMovement.x, 0f, inputMovement.y);
        return movement;
    }
}
