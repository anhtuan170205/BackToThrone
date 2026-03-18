using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private readonly int JUMP_HASH = Animator.StringToHash("Jump");
    private const float CROSS_FADE_DURATION = 0.1f;

    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        StateMachine.Animator.CrossFade(JUMP_HASH, CROSS_FADE_DURATION);
        StateMachine.ForceReceiver.SetVerticalVelocity(StateMachine.PlayerStats.JumpForce);
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement() * StateMachine.PlayerStats.MoveSpeed;
        movement += StateMachine.ForceReceiver.Movement;
        Move(movement, deltaTime);

        if (GetNormalizedTime(StateMachine.Animator, "Jump") >= 1f)
        {
            ReturnToLocomotion();
        }
    }

    public override void Exit() { }

    private Vector3 CalculateMovement()
    {
        Vector2 inputMovement = StateMachine.InputReader.MovementValue;
        Vector3 movement = new Vector3(inputMovement.x, 0f, inputMovement.y);
        return movement;
    }
}
