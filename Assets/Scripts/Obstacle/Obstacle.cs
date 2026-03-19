using UnityEngine;

public class Obstacle : MonoBehaviour
{
	[SerializeField] private Collider _collider;
	[SerializeField] private ParticleSystem _explosionEffect;

	private void OnEnable()
	{
		if (_collider != null)
		{
			_collider.enabled = true;
		}
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
		}
	}

}
