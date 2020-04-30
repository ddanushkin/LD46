using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool playBackgroundMusicClip;
    [SerializeField] private AudioClip backgroundMusicClip;
    [SerializeField] private bool playAmbientClip;
    [SerializeField] private AudioClip ambientClip;

    public static GameManager Instance;
    
    private float _prevVolume;
    [Range(0f, 1f)]
    public float volume;

    [SerializeField] private GameObject playerGo;
    [SerializeField] private AudioClip levelUpCLip;
    [SerializeField] private int shurikensMagMax;
    public int huntersKilledCount;
    public int huntersKilledNextLimit;
    public bool huntersUpdateTimer;
    public int shurikensMag;
    public bool hunterSpawnerEnabled;
    
    private AudioSource _backgroundAudioSource;
    private AudioSource _ambientAudioSource;
    private HunterTreeSpawnerController spawner;
    
    void Awake() {
        Instance = this;
    }

    void Start()
    {
        spawner = GetComponent<HunterTreeSpawnerController>();
        _prevVolume = volume;
        if (playBackgroundMusicClip)
            _backgroundAudioSource = SoundManager.PlayMusic(backgroundMusicClip, true, volume);
        if (playAmbientClip)
            _ambientAudioSource = SoundManager.PlayMusic(ambientClip, true, volume);
        UIController.Instance.ShowMainMenuUI();
        hunterSpawnerEnabled = false;
    }

    private void Update()
    {
        if (volume != _prevVolume)
        {
            volume = Mathf.Clamp(volume, 0f, 1f);
            UpdateVolume(volume);
        }
        _prevVolume = volume;

        if (huntersKilledCount > huntersKilledNextLimit && huntersKilledCount % 5 == 0)
        {
            SoundManager.PlaySound(levelUpCLip);
            huntersUpdateTimer = true;
            huntersKilledNextLimit += 5;
            if (shurikensMag < shurikensMagMax)
                shurikensMag++;
        }
    }
    
    private void UpdateVolume(float newVolume)
    {
        if (_backgroundAudioSource != null)
            _backgroundAudioSource.volume = newVolume;
        if (_ambientAudioSource != null)
            _ambientAudioSource.volume = newVolume;
        SoundManager.UpdateVolume(newVolume);
    }
    public void StartGameLoop()
    {
        playerGo.transform.position = new Vector2(0, -10f);
        spawner.hunterSpawnTimerMin = 5f;
        spawner.hunterSpawnTimerMax = 10f;
        Time.timeScale = 1f;
        shurikensMag = 3;
        huntersKilledNextLimit = 1;
        UIController.Instance.HideMainMenuUI();
        UIController.Instance.ShowMainGameUI();
        hunterSpawnerEnabled = true;
    }
    
    public void ShowMainMenu()
    {
        UIController.Instance.HideMainGameUI();
        UIController.Instance.ShowMainMenuUI();
        hunterSpawnerEnabled = false;
    }
}
