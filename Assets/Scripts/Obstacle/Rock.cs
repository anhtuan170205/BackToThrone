using UnityEngine;
using Unity.Cinemachine;

public class Rock : MonoBehaviour
{
    [SerializeField] CinemachineImpulseSource _impulseSource;
    [SerializeField] private float _shakeMultiplier = 10f;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Hazard")) { return; }
        float distanceToPlayer = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = Mathf.Min(1f / distanceToPlayer * _shakeMultiplier, 1f);

        _impulseSource.GenerateImpulse(shakeIntensity);
    }
}
