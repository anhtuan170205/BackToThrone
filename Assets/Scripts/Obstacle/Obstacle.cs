using UnityEngine;

public class Obstacle : MonoBehaviour
{
	[SerializeField] private Collider _collider;
	[SerializeField] private ParticleSystem _explosionEffect;

	public ObstaclePool OwningPool { get; private set; }

	private void OnEnable()
	{
		if (_collider != null)
		{
			_collider.enabled = true;
		}
		GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
	}

	private void OnDisable()
	{
		GameManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
	}

	public void SetOwningPool(ObstaclePool pool)
	{
		OwningPool = pool;
	}

	public void ReturnToPool()
	{
		OwningPool.ReturnObjectToPool(this);
	}

	public void Explode()
	{
		if (_explosionEffect != null)
		{
			_explosionEffect.Play();
		}

		if (_collider != null)
		{
			_collider.enabled = false;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Explode();
			AudioManager.Instance.PlayExplosionSfx();
		}
	}

	private void HandleGameStateChanged(GameState newState)
	{
		if (newState != GameState.InGame)
		{
			ReturnToPool();
		}
	}
}
