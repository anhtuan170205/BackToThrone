using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action OnCollideWithHazard;
    public event Action OnCollideWithObstacle;

    private void OnCollisionEnter(Collision other) 
    {
        if (GameManager.Instance.CurrentGameState != GameState.InGame) { return; }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            OnCollideWithObstacle?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (GameManager.Instance.CurrentGameState != GameState.InGame) { return; }
        
        if (other.gameObject.CompareTag("Hazard"))
        {
            OnCollideWithHazard?.Invoke();
        }
    }

}
