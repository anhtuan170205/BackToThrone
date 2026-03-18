using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    [SerializeField] private float _rotationSpeed = 100f;

    private void Update()
    {
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(PLAYER_TAG)) return;
        OnPickup();
    }

    protected abstract void OnPickup();
}
