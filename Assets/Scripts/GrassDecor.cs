using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Random = UnityEngine.Random;

public class GrassDecor : MonoBehaviour
{
    [SerializeField] private SpriteAtlas decorAtlas;
    private Sprite[] _sprites;

    public GameObject decorElem;

    private Camera _camera;
    private float _startY;
    private float _minX;
    private float _maxX;
    
    public float timer;

    void Start()
    {
        _sprites = new Sprite[decorAtlas.spriteCount];
        decorAtlas.GetSprites(_sprites);
        
        _camera = Camera.main;
        _startY = _camera.ViewportToWorldPoint(new Vector3(1, 1, 0)).y + 2;
        _minX = _camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + 1;
        _maxX = _camera.ViewportToWorldPoint(new Vector3(1, 1, 0)).x - 1;
        timer = 2f;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            CreateDecor();
            timer = 2f;
        }
    }

    private void CreateDecor()
    {
        
        Sprite newSprite = _sprites[Random.Range(0, _sprites.Length)];
        GameObject newDec = Instantiate(decorElem, 
                    new Vector3(Random.Range(_minX, _maxX), _startY, 0), decorElem.transform.rotation);
        newDec.GetComponent<SpriteRenderer>().sprite = newSprite;
    }
}
