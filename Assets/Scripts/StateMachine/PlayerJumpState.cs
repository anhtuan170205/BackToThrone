using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private readonly int JUMP_HASH = Animator.StringToHash("Jump");
    private const float CROSS_FADE_DURATION = 0.1f;

    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        StateMachine.Animator.CrossFade(JUMP_HASH, CROSS_FADE_DURATION);
        Vector3 jumpVelocity = Vector3.up * StateMachine.PlayerStats.JumpForce;
        StateMachine.ForceReceiver.AddForce(jumpVelocity);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(StateMachine.Animator, "Jump") >= 1f)
        {
            ReturnToLocomotion();
        }
    }

    public override void Exit() { }
}
