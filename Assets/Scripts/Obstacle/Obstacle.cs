using UnityEngine;

public class Obstacle : MonoBehaviour
{
	[SerializeField] private Collider _collider;
	[SerializeField] private ParticleSystem _explosionEffect;

	public ObstaclePool OwningPool { get; private set; }
	
	private void Start()
	{
		GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
	}

	private void OnDestroy()
	{
		GameManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
	}

	private void OnEnable()
	{
		if (_collider != null)
		{
			_collider.enabled = true;
		}
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

		if (collision.gameObject.CompareTag("Void"))
		{
			ReturnToPool();
		}
	}

	private void HandleGameStateChanged(GameState newState)
	{
		if (newState == GameState.GameOver)
		{
			ReturnToPool();
		}
	}

	private void Update()
	{
		if (transform.position.z < Camera.main.transform.position.z - 10f)
		{
			ReturnToPool();
		}
	}
}
