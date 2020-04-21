using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SandclockAnim : MonoBehaviour
{
    
    private enum ClockState
    {
        InProgress,
        Full,
        Empty,
        Rotation,
    }
    
    [SerializeField] private SpriteAtlas sandclockAtlas;
    // 1 - empty
    // 0 - full up part
    // 3 - progress 1 
    // 2 - progress 2
    // 5 - progress 3
    // 4 - full down part

    [SerializeField] private Image timeImage;
    private Sprite[] _clockSet;
    private Dictionary<int, Sprite> _clockSetAnim;

    [SerializeField] private ClockState clockState;
    [SerializeField] private TimeSlowdownController timeController;

    [SerializeField] private AudioClip readySound;
    
    private float _speedChanges = 1;
    private int _indexAnim;
    private float _angle;
    private float _markTime = 0;

    void Start()
    {
        clockState = ClockState.Full;
        
        _clockSet = new Sprite[sandclockAtlas.spriteCount];
        sandclockAtlas.GetSprites(_clockSet);
        
        _clockSetAnim = new Dictionary<int, Sprite>();
        _clockSetAnim.Add(0, _clockSet[3]);
        _clockSetAnim.Add(1, _clockSet[2]);
        _clockSetAnim.Add(2,_clockSet[5]);
        _clockSetAnim.Add(3,_clockSet[4]);
        
        timeImage.sprite = _clockSet[0];
        timeImage.SetNativeSize();
    }
    
    void Update()
    {
        if (timeController.state == TimeSlowdownController.PowerState.None && clockState != ClockState.Full)
            SignFulling();
        
        if ((timeController.state == TimeSlowdownController.PowerState.Decrease || 
                 timeController.state == TimeSlowdownController.PowerState.Increase ||
                        timeController.state == TimeSlowdownController.PowerState.Hold) && clockState != ClockState.Rotation) 
            clockState = ClockState.InProgress;
        
        if (timeController.state == TimeSlowdownController.PowerState.Reload)
            clockState = ClockState.Empty;
        
        SetClock();
    }

    private void SignFulling()
    {
        _markTime += Time.deltaTime;
        if (_markTime > 0 && _markTime < 0.1)
            timeImage.rectTransform.rotation = Quaternion.Euler(0, 0, -30);
        if (_markTime > 0.1 && _markTime < 0.2)
            timeImage.rectTransform.rotation = Quaternion.Euler(0, 0, 30);
        if (_markTime > 0.2 && _markTime < 0.3)
            timeImage.rectTransform.rotation = Quaternion.Euler(0, 0, 0);
        if (_markTime > 0.3 && _markTime < 0.4)
            timeImage.rectTransform.localScale = new Vector3(2, 2, 2);
        if (_markTime > 0.5)
        {
            timeImage.rectTransform.localScale = new Vector3(1, 1, 1);
            clockState = ClockState.Full;
            SoundManager.PlaySound(readySound);
            _markTime = 0;
        }
    }

    private void SetClock()
    {
        if (clockState == ClockState.Empty)
            timeImage.sprite = _clockSet[1];
        if (clockState == ClockState.Full)
            timeImage.sprite = _clockSet[0];
        if (clockState == ClockState.InProgress)
            AnimClock();
        if (clockState == ClockState.Rotation)
            RotateClock();
    }

    private void AnimClock()
    {
        _speedChanges -= Time.unscaledDeltaTime * 4;
        if (_speedChanges < 0)
        {
            if (_indexAnim == 3)
                clockState = ClockState.Rotation;
            timeImage.sprite = _clockSetAnim[_indexAnim];
            _indexAnim++;
            _speedChanges = 1;
        }
    }

    private void RotateClock()
    {
        _angle += Time.unscaledDeltaTime * 1000;
        if (_angle > 180)
        {
            clockState = ClockState.InProgress;
            timeImage.sprite = _clockSet[0];
            timeImage.rectTransform.rotation = Quaternion.Euler(0, 0, 0);
            _indexAnim = 0;
            _angle = 0;
            return;
        }
        timeImage.rectTransform.rotation = Quaternion.Euler(0, 0, _angle);
    }
}
