using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Random = UnityEngine.Random;

public class RiverAnim : MonoBehaviour
{
    [SerializeField] private SpriteAtlas spriteAtlas;
    private Sprite[] _rivers;
    public GameObject riverPrefab;
    private GameObject _newRiver;

    private float _counter;
    private Sprite _currentSprite;

    private int _flag = 0;

    private Camera _camera;
    private float _startY;

    private float _randomSpawner;
    
    void Start()
    {
        _rivers = new Sprite[spriteAtlas.spriteCount];
        spriteAtlas.GetSprites(_rivers);
        _counter = 2f;
        
        _camera = Camera.main;
        _startY = _camera.ViewportToWorldPoint(new Vector3(1, 1, 0)).y + 2;
        _newRiver = Instantiate(riverPrefab, new Vector3(0, _startY, 0), riverPrefab.transform.rotation);
    }

    void Update()
    {
        _counter -= Time.deltaTime;
        if (_counter < 0)
        {
            RiverAnimated();
            _counter = 1f;
        }
    }

    private IEnumerator RandomTimeSpawn()
    {
        _randomSpawner = Random.Range(0, 15);
        while (_randomSpawner > 0)
        {
            _randomSpawner -= Time.deltaTime;
            yield return null;
        }
        _newRiver = Instantiate(riverPrefab, new Vector3(0, _startY, 0), riverPrefab.transform.rotation);
        yield return _randomSpawner;
    }

    private void RiverAnimated()
    {
        if (_flag == 0)
        {
            _currentSprite = _rivers[1];
            _flag = 1;
        }
        else if (_flag == 1)
        {
            _currentSprite = _rivers[0];
            _flag = 0;
        }

        if (_newRiver == null)
            StartCoroutine(RandomTimeSpawn());
        else
            _newRiver.GetComponent<SpriteRenderer>().sprite = _currentSprite;
        
    }
}
