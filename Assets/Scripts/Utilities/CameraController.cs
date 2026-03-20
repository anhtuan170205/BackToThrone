using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _menuCamera;
    [SerializeField] private CinemachineCamera _inGameCamera;
    [SerializeField] private CinemachineCamera _transitionCamera;
    [SerializeField] private ParticleSystem _speedUpEffect;
    [SerializeField] private float _minFOV = 20f;
    [SerializeField] private float _maxFOV = 80f;
    [SerializeField] private float _fovSmoothSpeed = 5f;
    [SerializeField] private float _zoomStep = 5f;
    [SerializeField] private float _transitionDuration = 0.3f;

    [SerializeField] private int _activePriority = 10;
    [SerializeField] private int _inactivePriority = 0;

    private float _targetFOV;
    private Coroutine _transitionCoroutine;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
        LevelGenerator.Instance.OnSpeedUp += HandleSpeedUp;

        _targetFOV = _inGameCamera.Lens.FieldOfView;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void LateUpdate()
    {
        float currentFOV = _inGameCamera.Lens.FieldOfView;
        float newFOV = Mathf.Lerp(currentFOV, _targetFOV, _fovSmoothSpeed * Time.deltaTime);

        _inGameCamera.Lens.FieldOfView = Mathf.Clamp(newFOV, _minFOV, _maxFOV);
    }

    private void HandleGameStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.MainMenu:
                ActivateCamera(_menuCamera);
                break;
            case GameState.InGame:
                StartTransitionToInGame();
                break;
            case GameState.GameOver:
                ActivateCamera(_menuCamera);
                break;
        }
    }

    private void ActivateCamera(CinemachineCamera camera)
    {
        _menuCamera.Priority = _inactivePriority;
        _inGameCamera.Priority = _inactivePriority;
        _transitionCamera.Priority = _inactivePriority;

        camera.Priority = _activePriority;
    }

    private void StartTransitionToInGame()
    {
        _transitionCoroutine = StartCoroutine(TransitionCoroutine());
    }

    private IEnumerator TransitionCoroutine()
    {
        ActivateCamera(_transitionCamera);
        yield return new WaitForSeconds(_transitionDuration);
        ActivateCamera(_inGameCamera);
        _transitionCoroutine = null;
    }

    private void HandleSpeedUp(float amount)
    {
        if (!_speedUpEffect.isPlaying && amount > 0f)
        {
            _speedUpEffect.Play();
        }

        _targetFOV += amount * _zoomStep;
        _targetFOV = Mathf.Clamp(_targetFOV, _minFOV, _maxFOV);
    }

    private void ResetFOV()
    {
        _targetFOV = _minFOV;
    }
}
