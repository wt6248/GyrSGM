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
    public uint _damage = 1;
    // speed of bullet
    public float _speed = 1f;

    // 총알 개수
    public uint _pelletCount = 8;
    // Spreading angle
    public float _spreadAngle = 15f;
    // bullet direction
    public Vector3 _dir = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
