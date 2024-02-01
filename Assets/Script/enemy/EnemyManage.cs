using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManage : MonoBehaviour
{
    // list of enemy position
    public List<GameObject> enemyList;
    const float generateCoolTime = 5f;
    const float spawnRadious = 10f;
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
            enemyList.Add(instance);
            // print(Random.insideUnitCircle.normalized);
        }
    }
}
