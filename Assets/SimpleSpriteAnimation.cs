using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SimpleSpriteAnimation : MonoBehaviour
{
    [SerializeField] private SpriteAnimationObject animationObject;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private bool autoStart;
    [SerializeField] private bool loopAnimation;
    [SerializeField] private int framesPerSecond;
    
    private float _timerMax;
    private float _timer;
    private int _currentFrame;
    private int _framesCount;
    private Sprite _currentSprite;
    private Sprite[] _animationFrames;
    private bool _animationStopped;
    private bool _skipFirst = true;
    
    void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null || animationObject == null || animationObject.Sprites.Length == 0)
            return;
        _framesCount = animationObject.Sprites.Length;
        _animationFrames = animationObject.Sprites;
        _timerMax = 1f / framesPerSecond;
        _animationStopped = !autoStart;
        Reset();
    }
    
    void Update()
    {
        if (_skipFirst)
        {
            _skipFirst = false;
            return;
        }
        if (_animationStopped)
            return;
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            spriteRenderer.sprite = _animationFrames[_currentFrame];
            _currentFrame++;
            if (_currentFrame >= _framesCount)
            {
                if (loopAnimation)
                    _currentFrame %= _framesCount;
                else
                {
                    Reset();
                    _animationStopped = true;
                }
            }
            _timer = _timerMax + _timer;
        }
    }

    public void Play()
    {
        Reset();
        _animationStopped = false;
    }
    
    public void Stop()
    {
        Reset();
        _animationStopped = true;
    }
    
    public void Reset()
    {
        _timer = _timerMax;
        _currentFrame = 0;
    }
}
