using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    private readonly int IDLE_HASH = Animator.StringToHash("Idle");
    private const float CROSS_FADE_DURATION = 0.1f;
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        StateMachine.Animator.CrossFade(IDLE_HASH, CROSS_FADE_DURATION);
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void Exit() { }
}
