using System;
using UnityEngine;

public class ShurikenController : MonoBehaviour
{
	public float speed;
	private Vector3 _direction;
	[SerializeField] private float lifeTime;
	[SerializeField] private GameObject sparkPrefab;
	[SerializeField] private GameObject healPrefab;
	[SerializeField] private AudioClip onHealClip;
	[SerializeField] private AudioClip onCollisionClip;
	private void Start()
	{
		Camera camera = Camera.main;
		Vector3 screenTouchPosition = Input.mousePosition;
		Vector3 touchPosition = camera.ScreenToWorldPoint(screenTouchPosition);
		_direction = (touchPosition - transform.position).normalized;
		_direction.z = 0;
	}
    
	void Update()
	{
		if (lifeTime <= 0.0f)
			Destroy(transform.gameObject);
		lifeTime -= Time.deltaTime;
		transform.position += _direction * speed * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("shuriken"))
			return;
		if (other.CompareTag("balloon"))
		{
			other.GetComponent<HoleSpawner>().SpawnPatch();
			Instantiate(healPrefab, transform.position, Quaternion.identity);
			SoundManager.PlaySound(onHealClip);
		}
		else
		{
			Instantiate(sparkPrefab, transform.position, Quaternion.identity);
			SoundManager.PlaySound(onCollisionClip, true);
		}
		Destroy(gameObject);
	}
}