using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _drag = 0.3f;

    private Vector3 _dampingVelocity;
    private Vector3 _impact;
    private float _verticalVelocity;

    public Vector3 Movement => _impact + Vector3.up * _verticalVelocity;

    private void Update()
    {
        if (_controller.isGrounded && _verticalVelocity < 0)
        {
            _verticalVelocity = -2f;
        }
        else
        {
            _verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
        _impact = Vector3.SmoothDamp(_impact, Vector3.zero, ref _dampingVelocity, _drag);
    }

    public void AddForce(Vector3 force)
    {
        _impact += force;
    }

    public void ResetForce()
    {
        _impact = Vector3.zero;
        _verticalVelocity = 0f;
    }

    public void SetVerticalVelocity(float velocity)
    {
        _verticalVelocity = velocity;
    }
}
