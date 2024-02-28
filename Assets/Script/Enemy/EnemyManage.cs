using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyManage : MonoBehaviour
{
    // GameObject enemy;

    // GameObject enemy_speed;
    // GameObject enemy_hp;

    public GameObject player;
    [Header("Generate Policy")]
    public int _generateUnitNumber;
    [SerializeField][Range(0f, 5f)] public float _enemyGenerateCoolTime, _enemySpawnRadious;
    public float _enemyGenerateCooldown = 0;
    public Vector3 _enemySpawnSizeOuter, _enemySpawnSizeInner;
    public Vector3Int _enemyProbability; // (1, 1, 1)
    public RatioTable _ratioTable = new();

    public float elapsedTime = 0f;
    public bool increasedSpawnRate = false;
    public float minimumDistance = 50f;



    // Start is called before the first frame update
    void Start()
    {
        // initialize initial enemies
        // enemyList.Add(/*TODO*/);

        // enemy = Resources.Load("Prefabs/Enemy") as GameObject;

        // enemy_speed = Resources.Load("Prefabs/EnemySpeed") as GameObject;
        // enemy_hp = Resources.Load("Prefabs/EnemyHP") as GameObject;

        player = GameObject.Find("Main Character");
        _ratioTable.Add("Prefabs/Enemy", _enemyProbability.x);
        _ratioTable.Add("Prefabs/EnemySpeed", _enemyProbability.y);
        _ratioTable.Add("Prefabs/EnemyHP", _enemyProbability.z);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        // 1분 30초가 지난 후에 적 생성량 증가
        if (elapsedTime >= 90f && !increasedSpawnRate)
        {
            _generateUnitNumber *= 5; // 적 생성량을 5배로 증가시킴
            increasedSpawnRate = true; // 증가 상태를 true로 변경하여 중복 증가 방지

            Debug.Log("Spawn rate increased!"); // 증가 확인을 위한 디버그 메시지
        }

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
            // TODO : spawn enemy outside the map, not circle

            // spawn under the probability (base, speed, hp) -> (1, 1, 1) base

            for (int i = 0; i < _generateUnitNumber; i++)
            {
                // int unitIdx = GetUnitID(_enemyProbability);
                // GameObject instance;
                // if (unitIdx == 0)
                //     instance = Instantiate(enemy, _enemySpawnRadious * Random.insideUnitCircle.normalized, Quaternion.identity);
                // else if (unitIdx == 1)
                //     instance = Instantiate(enemy_speed, _enemySpawnRadious * Random.insideUnitCircle.normalized, Quaternion.identity);
                // else
                //     instance = Instantiate(enemy_hp, _enemySpawnRadious * Random.insideUnitCircle.normalized, Quaternion.identity);

                GameObject enemy = _ratioTable.GetRandomGameobject();
                Vector3 spawnRange = GetRandomSpawnPosition();                
                GameObject instance = Instantiate(enemy, spawnRange/2, Quaternion.identity);
            }
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        // 적이 생성될 위치를 무작위로 결정하여 반환
        Vector3 spawnRange = new(Random.Range(_enemySpawnSizeInner.x, _enemySpawnSizeOuter.x),
                                           Random.Range(_enemySpawnSizeInner.y, _enemySpawnSizeOuter.y),
                                           0);
        
        float distance = Vector3.Distance(spawnRange, player.transform.position);
        
        if(distance >= minimumDistance)
        {
            return spawnRange;
        }
        else
        {
            spawnRange += new Vector3(50f,50f,50f);
            return spawnRange;
        }        
    }

    int GetUnitID(Vector3 probVec)
    { // TODO: change to array
        int selectedIndex;
        Vector3 normalizedProb = ToRatio(_enemyProbability);
        //Debug.Log(normalizedProb);
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
        //Debug.Log(randomValue);
        //Debug.Log(selectedIndex);

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

