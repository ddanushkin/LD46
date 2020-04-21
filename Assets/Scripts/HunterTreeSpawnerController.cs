using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterTreeSpawnerController : MonoBehaviour
{
    [SerializeField] private List<GameObject> treePrefabList;
    [SerializeField] private float treeSpawnTimerMax;
    [SerializeField] private Vector3 leftTreeOffset;
    [SerializeField] private Vector3 rightTreeOffset;
    [SerializeField] private float deltaZ;
    
    private Vector2 _topRightViewPoint;
    private Vector2 _bottomLeftViewPoint;
    private Vector3 _leftSpawnPosition;
    private Vector3 _rightSpawnPosition;
    private float _treeSpawnTimer;
    
    [SerializeField] private bool hunterSpawnerEnabled;
    [SerializeField] private GameObject hunterPrefab;
    [SerializeField] private float hunterSpawnTimerMin;
    [SerializeField] private float hunterSpawnTimerMax;
    [SerializeField] private Vector3 leftHunterOffset;
    [SerializeField] private Vector3 rightHunterOffset;
    
    private float _hunterSpawnTimer;
    
    void Start()
    {
        _treeSpawnTimer = treeSpawnTimerMax;
        _hunterSpawnTimer = Random.Range(hunterSpawnTimerMin, hunterSpawnTimerMax);
        _topRightViewPoint = Camera.main.ViewportToWorldPoint(Vector2.one);
        _bottomLeftViewPoint = Camera.main.ViewportToWorldPoint(Vector2.zero);
        _leftSpawnPosition = new Vector3(_bottomLeftViewPoint.x, _topRightViewPoint.y, 0f);
        _rightSpawnPosition = new Vector3(_topRightViewPoint.x, _topRightViewPoint.y, 0f);
    }
    
    void Update()
    {
        _treeSpawnTimer -= Time.deltaTime;
        _hunterSpawnTimer -= Time.deltaTime;
        GameObject leftTree = null;
        GameObject rightTree = null;
        if (_treeSpawnTimer <= 0)
        {
            leftTree = SpawnTree(ref _leftSpawnPosition, leftTreeOffset, deltaZ, treePrefabList);
            rightTree = SpawnTree(ref _rightSpawnPosition, rightTreeOffset, deltaZ, treePrefabList);
            _treeSpawnTimer = treeSpawnTimerMax;
        }
        
        if (_hunterSpawnTimer <= 0.0f && hunterSpawnerEnabled)
        {
            if (SpawnHunter(Random.value > 0.5f, leftTree, rightTree))
                _hunterSpawnTimer = Random.Range(hunterSpawnTimerMin, hunterSpawnTimerMax);
        }
    }

    GameObject SpawnTree(ref Vector3 position, Vector3 offset, float depthFactor, List<GameObject> treePrefabList)
    {
        if (treePrefabList.Count > 0)
        {
            GameObject prefab = treePrefabList[Random.Range(0, treePrefabList.Count)];
            GameObject treeGo = Instantiate(prefab, position + offset, Quaternion.identity, transform);
            treeGo.name = "TreeNoHunter";
            position.z += depthFactor;
            return treeGo;
        }
        return null;
    }

    bool SpawnHunter(bool isLeft, GameObject leftParent, GameObject rightParent)
    {
        GameObject parentGo = isLeft ? leftParent : rightParent;
        if (parentGo == null)
            return false;
        Vector3 spawnPosition = isLeft ?
            parentGo.transform.Find("LeftSpawnPoint").position : parentGo.transform.Find("RightSpawnPoint").position;
        GameObject hunterGo = Instantiate(hunterPrefab, spawnPosition, Quaternion.identity, parentGo.transform);
        hunterGo.GetComponentInChildren<HunterController>().isLeft = isLeft;
        parentGo.name = "TreeWithHunter";
        if (!isLeft)
        {
            Vector3 scale = hunterGo.transform.localScale;
            scale.x *= -1;
            hunterGo.transform.localScale = scale;
        }
        return true;
    }
}
