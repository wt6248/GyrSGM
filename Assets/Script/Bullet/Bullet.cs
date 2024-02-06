using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // ref : https://namu.wiki/w/%EC%82%B0%ED%83%84%EC%B4%9D#s-6.1
    public enum BulletType
    {
        Gauge10,
        Gauge12,
        Gauge20,
        Gauge28
    }

    // damage of bullet
    public uint _damage;
    // speed of bullet
    public float _speed;
    // bullet direction
    public Vector3 _dir;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
