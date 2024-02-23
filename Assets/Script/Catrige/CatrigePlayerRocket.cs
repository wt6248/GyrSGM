using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatrigePlayerRocket : Catrige
{
    public float _explosionRadious;

    protected override void GenerateBullet(Vector3 bulletDirection, float dmgMultiplyer, Entity.EntityType attackableEntityType, Vector3 generationPosition)
    {
        //총알 프리팹을 생성한다. instantiate
        GameObject instance = Instantiate(_bulletGameObject, generationPosition, Quaternion.identity);
        //총알을 초기화하고 파괴 시간을 지정한다. activate
        instance.GetComponent<BulletScript>().Activate(_bulletSpeed, bulletDirection, _bulletDamage*dmgMultiplyer, _knockbackDistance, _duration, _maxPenetration, attackableEntityType);
        instance.GetComponent<BulletRocket>()._explosionRadious = _explosionRadious;
    }
}
