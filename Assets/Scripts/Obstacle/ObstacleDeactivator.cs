using UnityEngine;

public class ObstacleDeactivator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Obstacle")) return;

        Obstacle obstacle = other.GetComponent<Obstacle>();
        obstacle.ReturnToPool();
    }
}
