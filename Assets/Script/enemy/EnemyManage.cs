using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManage : MonoBehaviour
{
    const float generateCoolTime = 5f;
    const float spawnRadious = 1f;
    float generateCooldown = 0;
    GameObject enemy;


    // Start is called before the first frame update
    void Start()
    {
        // initialize initial enemies
        // enemyList.Add(/*TODO*/);

        enemy = Resources.Load("Prefabs/Enemy") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (0 < generateCooldown)
        {
            generateCooldown -= Time.deltaTime;
        }
        else
        {
            generateCooldown = generateCoolTime;
            GameObject instance = Instantiate(enemy, spawnRadious * Random.insideUnitCircle.normalized, Quaternion.identity);
        }
    }
}
