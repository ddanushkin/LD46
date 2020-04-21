using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleSpawner : MonoBehaviour
{
	private Transform _playerTransform;
	[SerializeField] private int holeCount;
	[SerializeField] private AudioClip newHoleClip;
	[SerializeField] private AudioClip onFirstHoleClip;
	
	private AudioSource deflateAudioSource;
	
	private void Start()
	{
		holeCount = 0;
		_playerTransform = transform.parent;
	}

	private void Update()
	{
		if (holeCount > 0)
		{
			Vector3 playerPosition = _playerTransform.position;
			playerPosition.y -= holeCount * 0.05f * Time.deltaTime;
			_playerTransform.position = playerPosition;
			if (playerPosition.y <= -7f)
				Application.LoadLevel(Application.loadedLevel);
		}
	}

	public void SpawnNewHole()
	{
		foreach (Transform child in transform)
		{
			if (!child.gameObject.activeSelf)
			{
				holeCount++;
				if (holeCount == 1)
					deflateAudioSource = SoundManager.PlayMusic(onFirstHoleClip, true, GameManager.Instance.volume);
				if (holeCount > 1)
					deflateAudioSource.pitch += 0.15f;
				SoundManager.PlaySound(newHoleClip, true, 0.6f, 1.5f);
				child.gameObject.SetActive(true);
				return;
			}
		}
	}
}
