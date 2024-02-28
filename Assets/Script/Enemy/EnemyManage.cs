using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyManage : MonoBehaviour
{
    [Header("Generate Policy")]
    public int _generateUnitNumber;
    [SerializeField][Range(0f, 5f)] public float _enemyGenerateCoolTime, _enemySpawnRadious;
    public float _enemyGenerateCooldown = 0;
    public Vector3 _enemySpawnSizeOuter, _enemySpawnSizeInner;
    public Vector3Int _enemyProbability; // (1, 1, 1)
    public RatioTable _ratioTable = new();

    // Start is called before the first frame update
    void Start()
    {
        _ratioTable.Add("Prefabs/Enemy", _enemyProbability.x);
        _ratioTable.Add("Prefabs/EnemySpeed", _enemyProbability.y);
        _ratioTable.Add("Prefabs/EnemyHP", _enemyProbability.z);
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

            // spawn under the probability (base, speed, hp) -> (1, 1, 1) base
            for (int i = 0; i < _generateUnitNumber; i++)
            {
                GameObject enemy = _ratioTable.GetRandomGameobject();
                Vector3 spawnRange = new(Random.Range(_enemySpawnSizeInner.x, _enemySpawnSizeOuter.x),
                                           Random.Range(_enemySpawnSizeInner.y, _enemySpawnSizeOuter.y),
                                           -0.1f);
                GameObject instance = Instantiate(enemy, spawnRange / 2, Quaternion.identity);
            }
        }
    }
}
