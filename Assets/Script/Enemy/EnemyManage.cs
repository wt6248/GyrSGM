using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManage : MonoBehaviour
{
    // GameObject enemy;

    // GameObject enemy_speed;
    // GameObject enemy_hp;

    [Header("Generate Policy")]
    public int _generateUnitNumber;
    [SerializeField][Range(0f, 5f)] public float _enemyGenerateCoolTime, _enemySpawnRadious;
    public float _enemyGenerateCooldown = 0;
    public Vector3 _enemySpawnSize;
    public Vector3 _enemyProbability; // (1, 1, 1)
    public RatioTable _ratioTable = new();



    // Start is called before the first frame update
    void Start()
    {
        // initialize initial enemies
        // enemyList.Add(/*TODO*/);

        // enemy = Resources.Load("Prefabs/Enemy") as GameObject;

        // enemy_speed = Resources.Load("Prefabs/EnemySpeed") as GameObject;
        // enemy_hp = Resources.Load("Prefabs/EnemyHP") as GameObject;

        _ratioTable.Add("Prefabs/Enemy", 3);
        _ratioTable.Add("Prefabs/EnemySpeed", 2);
        _ratioTable.Add("Prefabs/EnemyHP", 1);
    }

    // Update is called once per frame
    void Update()
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
                GameObject instance = Instantiate(enemy, _enemySpawnRadious * Random.insideUnitCircle.normalized, Quaternion.identity);
            }
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
