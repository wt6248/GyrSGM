using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSlug : BulletScript
{
    // Start is called before the first frame update
    void Start()
    {
        _damage = 1;
        _speed = 2f;
        _pelletCount = 1;
        _spreadAngle = 0f;
        _duration = 3f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
