using System;
using UnityEngine;

public class ShurikenController : MonoBehaviour
{
	public float speed;
	private Vector3 _direction;
	[SerializeField] private float lifeTime;
	[SerializeField] private GameObject sparkPrefab;
	[SerializeField] private AudioClip onCollisionAudio;
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
		if (other.CompareTag("balloon"))
			other.GetComponent<HoleSpawner>().SpawnNewHole();
		Instantiate(sparkPrefab, transform.position, Quaternion.identity);
		SoundManager.PlaySound(onCollisionAudio, true);
		Destroy(gameObject);
	}
}