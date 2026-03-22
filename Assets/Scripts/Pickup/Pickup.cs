using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    private const string PICKUP_TAG = "Pickup";
    [SerializeField] private float _rotationSpeed = 100f;

    private void Update()
    {
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(PICKUP_TAG)) return;
        OnPickup();
    }

    protected abstract void OnPickup();
}
