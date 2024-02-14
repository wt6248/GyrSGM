using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManage : MonoBehaviour
{

    public Vector3 _enemySpawnSize;
    public float _enemyGenerateCooldown = 0;
    GameObject enemy;

    GameObject enemy_speed;
    GameObject enemy_hp;

    [Header ("Generate Policy")]
    public float _enemyGenerateCoolTime; // 5f
    public float _enemySpawnRadious; // 4f
    public int _generateUnitNumber;
    public Vector3 _enemyProbability; // (0,3, 0.3, 0.3)



    // Start is called before the first frame update
    void Start()
    {
        // initialize initial enemies
        // enemyList.Add(/*TODO*/);

        enemy = Resources.Load("Prefabs/Enemy") as GameObject;

        enemy_speed = Resources.Load("Prefabs/EnemySpeed") as GameObject;
        enemy_hp = Resources.Load("Prefabs/EnemyHP") as GameObject;
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
            
            for (int i=0;i<_generateUnitNumber;i++){
                int unitIdx = getUnitID(_enemyProbability);
                GameObject instance;
                if (unitIdx == 0)
                    instance = Instantiate(enemy, _enemySpawnRadious * Random.insideUnitCircle.normalized, Quaternion.identity);
                else if (unitIdx == 1)
                    instance = Instantiate(enemy_speed, _enemySpawnRadious * Random.insideUnitCircle.normalized, Quaternion.identity);
                else
                    instance = Instantiate(enemy_hp, _enemySpawnRadious * Random.insideUnitCircle.normalized, Quaternion.identity);
            }
                

        }
    }
    int getUnitID(Vector3 probVec) { // TODO: change to array
        int selectedIndex;
        Vector3 normalizedProb = toRatio(_enemyProbability);
        Debug.Log(normalizedProb);
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
        Debug.Log(randomValue);
        Debug.Log(selectedIndex);
        return selectedIndex;
    }
    Vector3 toRatio(Vector3 inputVec) {
        Vector3 outVec = new Vector3(0f, 0f, 0f);
        float sum_of_elem = inputVec.x + inputVec.y + inputVec.z;
        outVec.x = inputVec.x / sum_of_elem;
        outVec.y = inputVec.y / sum_of_elem;
        outVec.z = inputVec.z / sum_of_elem;
        return outVec;
    }
    
}
