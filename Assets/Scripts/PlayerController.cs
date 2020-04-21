using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerController : MonoBehaviour
{
    private Camera _camera;
    private Transform _playerTransform;
    private float _shotTimer;
    private bool _timeScaleIsChanging;
    private Transform _ninjaTarget;
    
    [SerializeField] private int _shurikenNum;
    [SerializeField] private GameObject shurikenPrefab;
    [SerializeField] private Transform shurikenPlaceHolder;
    [SerializeField] private float reloadSpeed;
    [SerializeField] private float shurikenSpeed;
    [SerializeField] private AudioClip onShurikenInstantiate;
    [SerializeField] private AudioClip timeSpeedupAudioClip;
    [SerializeField] private AudioClip timeSlowdownAudioClip;
    [SerializeField] private UIController _uiController;

    [SerializeField] private SpriteAtlas ninjaAtlas;

    private Sprite[] _ninjaSet;
    private SpriteRenderer _spriteBody;
    private float _speedChanges;
    private bool _ifThrow;

    int _i;
    void Start()
    {
        _ninjaSet = new Sprite[ninjaAtlas.spriteCount];
        ninjaAtlas.GetSprites(_ninjaSet);
        _camera = Camera.main;
        _playerTransform = transform;
        _shotTimer = 0f;
        _shurikenNum = 3;
        _ninjaTarget = GameObject.FindGameObjectWithTag("ninja_target").transform;
        _spriteBody = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 screenTouchPosition = Input.mousePosition;
            Vector3 touchPosition = _camera.ScreenToWorldPoint(screenTouchPosition);
            _ninjaTarget.position = touchPosition;
        }

        _shotTimer -= Time.unscaledDeltaTime;
        if ((Input.GetButtonDown("Jump") || Input.GetMouseButtonUp(0)) && _shurikenNum > 0)
        {
            GameObject shuriken = Instantiate(shurikenPrefab, shurikenPlaceHolder.position, Quaternion.identity);
            ShurikenController sc = shuriken.GetComponent<ShurikenController>();
            sc.speed = shurikenSpeed;
            _ifThrow = true;
            _i = 2;
            _speedChanges = 0.5f;
            _shotTimer = reloadSpeed;
            SoundManager.PlaySound(onShurikenInstantiate);
            _shurikenNum--;
        }

        if (_shurikenNum == 0)
        {
            _uiController.SetSettings(reloadSpeed, _shotTimer);
        }

        if (_shurikenNum == 0 && _shotTimer <= 0.0f)
            _shurikenNum = 3;
        if (_ifThrow)
            ChangeSprite();
        _uiController.ChangeShurikenNum(_shurikenNum);
    }

    private void ChangeSprite()
    {
        _speedChanges -= Time.unscaledDeltaTime * 10;
        if (_speedChanges < 0)
        {
            _speedChanges = 0.5f;
            if (_i == -1)
            {
                
                _ifThrow = false;
                _spriteBody.sprite = _ninjaSet[1];
                return;
            }
            _spriteBody.sprite = _ninjaSet[_i];
            _i--;

        }
    }
}