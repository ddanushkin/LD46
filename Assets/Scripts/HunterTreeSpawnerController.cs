using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterTreeSpawnerController : MonoBehaviour
{
    [SerializeField] private List<GameObject> treePrefabList;
    [SerializeField] private float treeSpawnTimerMax;
    [SerializeField] private Vector3 leftSpawnPosition;
    [SerializeField] private Vector3 rightSpawnPosition;
    [SerializeField] private float deltaZ;
    
    private float _treeSpawnTimer;
    private int _prevLeftIndex;
    private int _prevRightIndex;
    
    [SerializeField] private GameObject hunterPrefab;
    public float hunterSpawnTimerMin;
    public float hunterSpawnTimerMax;

    private float _hunterSpawnTimer;
    
    void Start()
    {
        _treeSpawnTimer = treeSpawnTimerMax;
        _hunterSpawnTimer = Random.Range(hunterSpawnTimerMin, hunterSpawnTimerMax);

        int i = 0;
        while (i <= 25)
        {
            GameObject leftTree = SpawnTree(ref leftSpawnPosition, deltaZ, treePrefabList, ref _prevLeftIndex);
            leftTree.transform.position = new Vector3(
                leftSpawnPosition.x - i * 0.032f,
                leftSpawnPosition.y - i * 0.8f,
                0);
            GameObject rightTree = SpawnTree(ref rightSpawnPosition, deltaZ, treePrefabList, ref _prevRightIndex);
            rightTree.transform.position = new Vector3(
                rightSpawnPosition.x + i * 0.032f,
                rightSpawnPosition.y - i * 0.8f,
                0);
            i++;
        }

    }
    
    void Update()
    {
        _treeSpawnTimer -= Time.deltaTime;
        _hunterSpawnTimer -= Time.deltaTime;
        GameObject leftTree = null;
        GameObject rightTree = null;
        if (_treeSpawnTimer <= 0)
        {
            leftTree = SpawnTree(ref leftSpawnPosition, deltaZ, treePrefabList, ref _prevLeftIndex);
            rightTree = SpawnTree(ref rightSpawnPosition, deltaZ, treePrefabList, ref _prevRightIndex);
            _treeSpawnTimer = treeSpawnTimerMax;
        }
        
        if (_hunterSpawnTimer <= 0.0f && GameManager.Instance.hunterSpawnerEnabled)
        {
            if (SpawnHunter(Random.value > 0.5f, leftTree, rightTree))
                _hunterSpawnTimer = Random.Range(hunterSpawnTimerMin, hunterSpawnTimerMax);
        }

        if (GameManager.Instance.huntersUpdateTimer)
        {
            if (hunterSpawnTimerMin > 0.5f)
                hunterSpawnTimerMin -= 0.5f;
            if (hunterSpawnTimerMin < 0.5f)
                hunterSpawnTimerMin = 0.5f;
            if (hunterSpawnTimerMax > 1f)
                hunterSpawnTimerMax -= 1f;
            if (hunterSpawnTimerMax < 1f)
                hunterSpawnTimerMax = 1f;
            GameManager.Instance.huntersUpdateTimer = false;
        }
    }

    GameObject SpawnTree(ref Vector3 position, float depthFactor, List<GameObject> treePrefabList, ref int prevIndex)
    {
        if (treePrefabList.Count > 0)
        {
            int index = Random.Range(0, treePrefabList.Count);
            while (index == prevIndex)
                index = Random.Range(0, treePrefabList.Count);
            GameObject prefab = treePrefabList[index];
            GameObject treeGo = Instantiate(prefab, position, Quaternion.identity, transform);
            treeGo.name = "TreeNoHunter";
            position.z += depthFactor;
            prevIndex = index;
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
