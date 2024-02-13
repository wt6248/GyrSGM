using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScatter : BulletScript
{
    // Start is called before the first frame update
    void Start()
    {
        _damage = 1;
        _speed = 1f;
        _pelletCount = 8;
        _spreadAngle = 15f;
        _duration = 3f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
