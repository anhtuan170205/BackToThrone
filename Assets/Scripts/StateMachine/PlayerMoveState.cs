using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    private readonly int RUN_HASH = Animator.StringToHash("Run");
    private const float CROSS_FADE_DURATION = 0.1f;
    public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        StateMachine.Animator.CrossFade(RUN_HASH, CROSS_FADE_DURATION);
        AudioManager.Instance.PlayFootstepSfx();
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement() * StateMachine.PlayerStatProvider.MoveSpeed;
        movement = new Vector3(movement.x, movement.y, 0f);
        movement += StateMachine.ForceReceiver.Movement;
        
        Move(movement, deltaTime);
    }

    public override void Exit()
    {
        AudioManager.Instance.StopFootstepSfx();
    }

    private Vector3 CalculateMovement()
    {
        Vector2 inputMovement = StateMachine.InputReader.MovementValue;
        Vector3 movement = new Vector3(inputMovement.x, 0f, inputMovement.y);
        return movement;
    }
}
