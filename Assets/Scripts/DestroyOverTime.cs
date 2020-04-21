using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
	public float maxLifeTime = 2.5f;
	private float _lifeTime;

	void Start()
	{
		_lifeTime = maxLifeTime;
	}

	void Update()
	{
		if (_lifeTime <= 0.0f)
			Destroy(gameObject);
		_lifeTime -= Time.deltaTime;
	}
}