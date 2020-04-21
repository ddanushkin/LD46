using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HunterController : MonoBehaviour
{
	[SerializeField] private int maxHealth;
	[SerializeField] private List<AudioClip> onTriggerEnterAudioClips = new List<AudioClip>();
	[SerializeField] private List<AudioClip> onStartAudioClips = new List<AudioClip>();
	[SerializeField] private AudioClip deathAudioClip;
	[SerializeField] private AudioClip escapeAudioClip;
	[SerializeField] private GameObject hunterGo;
	[SerializeField] private Vector2 escapeDirection;
	
	private int _currentHealth;
	private bool _isDieing;
	private float _escapeTimer = 0.25f;
	private float _escapeSpeed = 5f;
	public bool isLeft;

	
	private void Start()
	{
		_isDieing = false;
		_currentHealth = maxHealth;
		if (onStartAudioClips.Count != 0)
			SoundManager.PlaySound(
				onStartAudioClips[Random.Range(0, onStartAudioClips.Count - 1)], true, 0.9f, 1.8f);
	}

	private void Update()
	{
		if (_isDieing)
		{
			_escapeTimer -= Time.deltaTime;
			hunterGo.transform.Translate(new Vector2(
				escapeDirection.x * (isLeft ? 1f : -1f) * _escapeSpeed * Time.deltaTime,
				escapeDirection.y * _escapeSpeed * Time.deltaTime));
			if (_escapeTimer <= 0.0f)
			{
				SoundManager.PlaySound(escapeAudioClip, true, 0.9f, 1.2f);
				UIController.Instance.ChangeHatNum(1);
				Destroy(hunterGo);
			}
		}
		else if (_currentHealth <= 0)
		{
			SoundManager.PlaySound(deathAudioClip, true, 0.9f, 1.5f);
			_isDieing = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (onTriggerEnterAudioClips.Count != 0)
		{
			SoundManager.PlaySound(
				onTriggerEnterAudioClips[Random.Range(0, onTriggerEnterAudioClips.Count - 1)], true, 0.9f, 1.3f);
			_currentHealth--;
		}
	}
}
