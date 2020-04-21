using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Object = UnityEngine.Object;

public class TimeSlowdownController : MonoBehaviour
{
	public enum PowerState
	{
		Increase,
		Decrease,
		Hold,
		Reload,
		Ready,
		None
	}
	
	private AudioSource _audioSource;
	[SerializeField] private AudioClip timeSpeedupAudioClip;
	[SerializeField] private AudioClip timeSlowdownAudioClip;
	[SerializeField] private AudioClip tickingAudioClip;
	[SerializeField] private float timeScaleMin;
	[SerializeField] private float timeScaleChangeSpeed;
	[SerializeField] private float powerHoldTimerMax;
	[SerializeField] private float powerReloadTimerMax;
	[SerializeField] private PostProcessVolume postProcessVolume;
	
	public PowerState state;
	private float _powerHoldTimer;
	private float _powerReloadTimer;
	private AudioSource _tickingAudioSource;
	private LensDistortion _lensDistortion;

	private float _startVolume;
	
	private void Start()
	{
		postProcessVolume.weight = 0f;
		postProcessVolume.profile.TryGetSettings(out _lensDistortion);
		state = PowerState.None;
		_powerHoldTimer = powerHoldTimerMax;
		_powerReloadTimer = powerHoldTimerMax;
	}

	private void Update()
	{
		if (state == PowerState.Ready)
		{
			_startVolume = GameManager.Instance.volume;
			StartCoroutine(ChangeValueFromToWithDuration(
				r => postProcessVolume.weight = r, 0f, 1f, 0.75f));
			StartCoroutine(ChangeIntencity(0f, -50f, 0.35f));
			SoundManager.PlaySound(timeSlowdownAudioClip);
			state = PowerState.Decrease;
		}
		
		if (state == PowerState.Decrease)
		{
			Time.timeScale -= timeScaleChangeSpeed * Time.unscaledDeltaTime;
			if (Time.timeScale <= timeScaleMin)
			{
				Time.timeScale = timeScaleMin;
				state = PowerState.Hold;
				_tickingAudioSource = SoundManager.PlayMusic(tickingAudioClip, true);
				return;
			}	
		}

		if (state == PowerState.Hold)
		{
			_powerHoldTimer -= Time.unscaledDeltaTime;
			if (_powerHoldTimer <= 0f)
			{
				_powerHoldTimer = powerHoldTimerMax;
				StartCoroutine(ChangeValueFromToWithDuration(
					r => postProcessVolume.weight = r, 1f, 0f, 0.75f));
				StartCoroutine(ChangeIntencity(0f, 50f, 0.35f));
				SoundManager.PlaySound(timeSpeedupAudioClip);
				state = PowerState.Increase;
				Destroy(_tickingAudioSource.gameObject);
				return;
			}
		}
		
		if (state == PowerState.Increase)
		{
			Time.timeScale += timeScaleChangeSpeed * Time.unscaledDeltaTime;
			if (Time.timeScale >= 1f)
			{
				Time.timeScale = 1f;
				state = PowerState.Reload;
				return;
			}	
		}
		
		if (state == PowerState.Reload)
		{
			_powerReloadTimer -= Time.unscaledDeltaTime;
			if (_powerReloadTimer <= 0f)
			{
				_powerReloadTimer = powerReloadTimerMax;
				state = PowerState.None;
			}
		}
		
	}

	public void PowerStart()
	{
		if (state == PowerState.None)
			state = PowerState.Ready;
	}
	
	IEnumerator ChangeIntencity(float vStart, float vEnd, float duration)
	{
		float elapsed = 0.0f;
		while (elapsed < duration)
		{
			_lensDistortion.intensity.value = Mathf.Lerp(vStart, vEnd, elapsed / duration);
			elapsed += Time.unscaledDeltaTime;
			yield return null;
		}
		_lensDistortion.intensity.value = vEnd;
		StartCoroutine(ChangeValueFromToWithDuration(
			r => _lensDistortion.intensity.value = r, _lensDistortion.intensity.value, 0f, duration));
	}

	public static IEnumerator ChangeValueFromToWithDuration(Action<float> reVar, float from, float to, float duration)
	{
		float elapsed = 0.0f;
		while (elapsed < duration)
		{
			reVar(Mathf.Lerp(from, to, elapsed / duration));
			elapsed += Time.unscaledDeltaTime;
			yield return null;
		}
		reVar(to);
	}
	
}