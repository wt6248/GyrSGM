using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyScatter : BulletScript
{
    // Start is called before the first frame update
    void Start()
    {
        //차후 inspector에서 knockbackDistance 값을 저장하면 아래는 삭제할 것.
        _knockbackDistance = 1f;
        _bulletPrefab = Resources.Load("Prefabs/BulletEnemyScatter") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = _speed * Time.deltaTime * _dir;
        transform.Translate(movement);
    }
}
