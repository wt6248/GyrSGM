using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManage : MonoBehaviour
{
    public float _enemyGenerateCoolTime = 5f;
    public float _enemySpawnRadious = 4f;
    public Vector3 _enemySpawnSize;
    public float _enemyGenerateCooldown = 0;
    GameObject enemy;
    GameObject enemySpeed;
    GameObject enemyHP;


    // Start is called before the first frame update
    void Start()
    {
        // initialize initial enemies
        // enemyList.Add(/*TODO*/);

        enemy = Resources.Load("Prefabs/Enemy") as GameObject;
        enemySpeed = Resources.Load("Prefabs/EnemySpeed") as GameObject;
        enemyHP = Resources.Load("Prefabs/EnemyHP") as GameObject;
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
            GameObject instance = Instantiate(enemySpeed, _enemySpawnRadious * Random.insideUnitCircle.normalized, Quaternion.identity);
        }
    }
}
