using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Vector3 _size = new(1, 2, 0);
    GameObject _map;
    GameObject _enemyManage;
    // Start is called before the first frame update
    void Start()
    {
        // set screen ratio
        Screen.SetResolution(1920, 1080, true);

        _map = GameObject.Find("Map");
        _map.transform.localScale = _size;

        /*
            TODO : send map size to EnemyManage
            EnemyManage will modify enemy spawn range
        */
        //_enemyManage = GameObject.FindObjectOfType<EnemyManage>();
        //_enemyManage._size = _size;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
