using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public Vector3 _size = new(1, 2, 0);
    GameObject _map;
    EnemyManage _enemyManage;
    // Start is called before the first frame update
    void Start()
    {
        // 30 means m_ReferencePixelsPerUnit in CanvasScale.cs
        _size = new(Screen.width / 30, Screen.height / 30, 0);

        _map = GameObject.Find("Map");
        _map.transform.localScale = _size;

        _enemyManage = GameObject.FindObjectOfType<EnemyManage>();
        _enemyManage._enemySpawnSizeOuter = _size;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
