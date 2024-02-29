using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyManage : MonoBehaviour
{

    public GameObject player;
    [Header("Generate Policy")]
    public int _generateUnitNumber;
    [SerializeField][Range(0f, 5f)] public float _enemyGenerateCoolTime, _enemySpawnRadious;
    public float _enemyGenerateCooldown = 0;
    //public Vector3 _enemySpawnSizeOuter, _enemySpawnSizeInner;
    public Vector3Int _enemyProbability; // (1, 1, 1)
    public RatioTable _ratioTable = new();

    public float _elapsedTime = 0f;
    public float _increaseRate = 0.55f;
    public int _initialGenerateUnitNumber;
    public float minimumDistance = 2f;

    float _worldHeight, _worldWidth;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Main Character");
        _ratioTable.Add("Prefabs/Enemy", _enemyProbability.x);
        _ratioTable.Add("Prefabs/EnemySpeed", _enemyProbability.y);
        _ratioTable.Add("Prefabs/EnemyHP", _enemyProbability.z);
       
        _initialGenerateUnitNumber = _generateUnitNumber;
        
        _worldHeight = 2 * Camera.main.orthographicSize;
        _worldWidth = _worldHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        //초기 생성량에 _increaseRate만큼 더하는 형태로 증가
        _elapsedTime += Time.deltaTime;
        _generateUnitNumber = _initialGenerateUnitNumber + Mathf.FloorToInt(_increaseRate * _elapsedTime);
        //Debug.Log("적 생성량 증가: " +_generateUnitNumber);

        SpawnEnemies();       
    }

    void SpawnEnemies()
    {
        if (0 < _enemyGenerateCooldown)
        {
            _enemyGenerateCooldown -= Time.deltaTime;
        }
        else
        {
            _enemyGenerateCooldown = _enemyGenerateCoolTime;

            // spawn under the probability (base, speed, hp) -> (1, 1, 1) base
            for (int i = 0; i < _generateUnitNumber; i++)
            {
                GameObject enemy = _ratioTable.GetRandomGameobject();
                Vector3 spawnRange = GetRandomSpawnPosition();                
                GameObject instance = Instantiate(enemy, spawnRange/2, Quaternion.identity);
            }
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        // 적이 생성될 위치를 무작위로 결정하여 반환
        Vector3 spawnRange = new(Random.Range(-_worldWidth, _worldWidth), Random.Range(-_worldHeight, _worldHeight), 0);
        float distance = Vector3.Distance(spawnRange, player.transform.position);
        
        if(distance >= minimumDistance)
        {
            return spawnRange;
        }
        else
        {
            spawnRange += (spawnRange - player.transform.position).normalized * minimumDistance;
            return spawnRange;
        }        
    }

    int GetUnitID(Vector3 probVec)
    { // TODO: change to array
        int selectedIndex;
        Vector3 normalizedProb = ToRatio(_enemyProbability);
        float randomValue = Random.Range(0f, 1f);
        if (randomValue < normalizedProb.x)
        {
            selectedIndex = 0;
        }
        else if (randomValue < normalizedProb.x + normalizedProb.y)
        {
            selectedIndex = 1;
        }
        else
        {
            selectedIndex = 2;
        }

        return selectedIndex;
    }
    Vector3 ToRatio(Vector3 inputVec)
    {
        Vector3 outVec = new Vector3(0f, 0f, 0f);
        float sum_of_elem = inputVec.x + inputVec.y + inputVec.z;
        outVec.x = inputVec.x / sum_of_elem;
        outVec.y = inputVec.y / sum_of_elem;
        outVec.z = inputVec.z / sum_of_elem;
        return outVec;
    }

}

