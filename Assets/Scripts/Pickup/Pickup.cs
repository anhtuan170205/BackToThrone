using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 100f;

    private void Update()
    {
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PlayerStatProvider>() == null) return;
        OnPickup();
    }

    protected abstract void OnPickup();
}
