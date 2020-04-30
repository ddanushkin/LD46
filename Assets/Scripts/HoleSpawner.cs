using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleSpawner : MonoBehaviour
{
	private enum State
	{
		Active,
        NonActive
	}
	
	private Transform _playerTransform;
	[SerializeField] private int holeCount;
	[SerializeField] private AudioClip newHoleClip;
	[SerializeField] private AudioClip newPatchClip;
	[SerializeField] private AudioClip onFirstHoleClip;
	[SerializeField] private AudioClip gameOverClip;
	
	private AudioSource deflateAudioSource;

	private State _state;

	private float playerSpeed = 0.25f;
	
	private void Start()
	{
		_state = State.NonActive;
		holeCount = 0;
		_playerTransform = transform.parent;
	}

	private void Update()
	{
		if (!GameManager.Instance.hunterSpawnerEnabled)
			return;
		
		if (holeCount > 0)
		{
			Vector3 playerPosition = _playerTransform.position;
			playerPosition.y -= holeCount * 0.05f * Time.deltaTime;
			_playerTransform.position = playerPosition;
			if (playerPosition.y <= -8f)
			{
				Time.timeScale = 1f;
				holeCount = 0;
				ResetPatches();
				_playerTransform.position = new Vector2(0, -10f);
				GameManager.Instance.ShowMainMenu();
				SoundManager.PlaySound(gameOverClip);
			}
		}
		
		if (holeCount == 0)
		{
			Vector3 playerPosition = _playerTransform.position;
			_playerTransform.position = new Vector2(
				playerPosition.x,
				Mathf.SmoothDamp(playerPosition.y, 0f, ref playerSpeed, 0.5f));
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

			if (child.gameObject.activeSelf && child.transform.GetChild(0).gameObject.activeSelf)
			{
				holeCount++;
				if (holeCount == 1)
					deflateAudioSource = SoundManager.PlayMusic(onFirstHoleClip, true, GameManager.Instance.volume);
				if (holeCount > 1)
					deflateAudioSource.pitch += 0.15f;
				SoundManager.PlaySound(newHoleClip, true, 0.6f, 1.5f);
				child.transform.GetChild(0).gameObject.SetActive(false);
				return;
			}
		}
	}

	public void ResetPatches()
	{
		foreach (Transform child in transform)
		{
			deflateAudioSource.Stop();
			Destroy(deflateAudioSource.gameObject);
			child.gameObject.SetActive(false);
			child.transform.GetChild(0).gameObject.SetActive(false);
		}
	}
	
	public void SpawnPatch()
	{
		foreach (Transform child in transform)
		{
			if (child.gameObject.activeSelf && !child.transform.GetChild(0).gameObject.activeSelf)
			{
				holeCount--;				
				if (holeCount == 1)
					deflateAudioSource.pitch -= 0.15f;
				if (holeCount == 0)
					Destroy(deflateAudioSource.gameObject);
				SoundManager.PlaySound(newPatchClip, true, 1f, 1.8f);
				child.transform.GetChild(0).gameObject.SetActive(true);
				return;
			}
		}
	}
}