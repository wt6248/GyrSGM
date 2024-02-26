using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyScatter : BulletScript
{
    // Start is called before the first frame update
    void Start()
    {
        _bulletPrefab = Resources.Load("Prefabs/BulletEnemyScatter") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = _speed * Time.deltaTime * _dir;
        transform.Translate(movement);
    }
}
