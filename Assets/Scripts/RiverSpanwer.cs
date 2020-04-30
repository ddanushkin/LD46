using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RiverSpanwer : MonoBehaviour
{
    public GameObject riverPrefab;
    private GameObject _newRiver;
    [SerializeField] private Vector3 spawnPosition;
    private float _randomSpawner;
    private bool _spawning;

    void Update()
    {
        if (_newRiver == null && !_spawning)
            StartCoroutine(RandomTimeSpawn());
    }

    private IEnumerator RandomTimeSpawn()
    {
        _randomSpawner = Random.Range(5, 15);
        _spawning = true;
        while (_randomSpawner > 0)
        {
            _randomSpawner -= Time.deltaTime;
            yield return null;
        }
        _spawning = false;
        _newRiver = Instantiate(riverPrefab, spawnPosition, riverPrefab.transform.rotation);
        yield return _randomSpawner;
    }
}
