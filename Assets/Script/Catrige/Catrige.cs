using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catrige : MonoBehaviour
{
    //차후 1번의 공격에 여러 탄환이 섞일경우, 아래의 내용을 리스트로 만들어야 할 수 있다.
    public BulletScript.BulletType _bulletType;
    public float _bulletDamage;
    public float _bulletSpeed;
    public float _spreadAngle;
    public uint _bulletPelletCount;
    //분산 각도
    //밀려나는 거리
    //탄의 지속시간
    //최대 관통 인원
    protected GameObject _bulletGameObject;

    // Start is called before the first frame update
    void Start()
    {
        GetBulletPrefab(_bulletType);
    }

    // Update is called once per frame
    void Update()
    {

    }
    //주어진 총알 발사함수를 따라 총알을 발사하는 함수이다.
    public void FireCatrige(Vector3 lineOfFire, Entity.EntityType attackableEntityType, Vector3 firePoint)
    {
        //총알이 나아갈 방향을 계산한다.
        //총알 갯수만큼 총알이 움직여야할 방향으로 GenerateBullet를 호출함.
        //weapon fire 관련 함수 참조
        for (int i = 0; i < _bulletPelletCount; i++)
        {
            GenerateBullet(lineOfFire, attackableEntityType, firePoint);
        }
    }

    //총알을 생성하는 함수이다.
    protected void GenerateBullet(Vector3 direction, Entity.EntityType attackableEntityType, Vector3 firePoint)
    {
        //TODO
        //총알 프리팹을 생성한다. instantiate
        //총알에 속도와 방향을 지정한다.
        //weapon fire 관련 함수 참조
        Vector3 rotatedFirePoint = Quaternion.AngleAxis(Vec2Angle(direction), Vector3.forward) * firePoint;
        GameObject instance = Instantiate(_bulletGameObject, transform.position + rotatedFirePoint, Quaternion.identity);
        Vector3 spreadVector = GetRandomErrorVector(direction);
        BulletScript bullet = instance.GetComponent<BulletScript>();
        bullet.SetVelocity(_bulletSpeed, spreadVector);
        bullet.Activate(attackableEntityType);
    }

    float Vec2Angle(Vector3 v) { return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg; }
    protected void GetBulletPrefab(BulletScript.BulletType bulletType)
    {
        //start에서 호출하는 함수이다.
        //bullet prefab를 받아와 저장해둔다.
        switch (bulletType)
        {
            case BulletScript.BulletType.PlayerSlug:
                _bulletGameObject = Resources.Load("Prefabs/BulletSlug") as GameObject;
                break;
            case BulletScript.BulletType.PlayerScatter:
                _bulletGameObject = Resources.Load("Prefabs/BulletScatter") as GameObject;
                break;
            case BulletScript.BulletType.PlayerRocket:
                _bulletGameObject = Resources.Load("Prefabs/BulletRocket") as GameObject;
                break;
            case BulletScript.BulletType.EnemyScatter:
                _bulletGameObject = Resources.Load("Prefabs/BulletEnemyScatter") as GameObject;
                break;
            default:
                _bulletGameObject = null;
                break;
        }
    }

    private Vector3 GetRandomErrorVector(Vector3 v)
    {
        float randomErrorAngle = Random.Range(_spreadAngle / 2, _spreadAngle / 2);
        return Quaternion.AngleAxis(randomErrorAngle, Vector3.forward) * v;
    }
}
