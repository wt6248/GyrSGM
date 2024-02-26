using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catrige : MonoBehaviour
{
    //차후 1번의 공격에 여러 탄환이 섞일경우, 아래의 변수들을 구조체 리스트로 만들어야 할 수 있다.
    public BulletScript.BulletType _bulletType;
    public float _bulletDamage;
    public float _bulletSpeed;
    public float _spreadAngle; //분산 각도
    public uint _bulletPelletCount; //1회 공격에 해당 타입의 총알 갯수
    public float _knockbackDistance; //적이 이 총알을 맞았을 때 밀려나는 거리
    public float _duration = 0.2f; //탄의 지속시간

    public uint _maxPenetration = 1; //최대 관통 인원
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
    //현재는 확산 각도를 바탕으로 산탄 공격만 정의되고 있다. for문과 spreadvector 부분
    //차후 다양한 형태의 공격에 따라 공격방법에 대한 스크립트나 함수, 클래스가 추가될 수 있다.
    public void FireCatrige(Vector3 lineOfFire, float damageMultiplier, Entity.EntityType attackableEntityType, Vector3 generationPosition)
    {
        //총알 갯수만큼 GenerateBullet를 호출함.
        for (int i = 0; i < _bulletPelletCount; i++)
        {
            //총알이 나아갈 방향을 계산한다.
            Vector3 spreadVector = GetRandomErrorVector(lineOfFire);
            //총알을 생성함.
            GenerateBullet(spreadVector, damageMultiplier, attackableEntityType, generationPosition);
        }
    }

    //총알을 생성하는 함수이다.
    protected virtual void GenerateBullet(Vector3 bulletDirection, float damageMultiplier, Entity.EntityType attackableEntityType, Vector3 generationPosition)
    {
        //총알 프리팹을 생성한다. instantiate
        GameObject instance = Instantiate(_bulletGameObject, generationPosition, Quaternion.identity);
        instance.GetComponent<BulletScript>().transform.Translate(new(0, 0, -0.1f));
        //총알을 초기화하고 파괴 시간을 지정한다. activate
        instance.GetComponent<BulletScript>().Activate(_bulletSpeed, bulletDirection, _bulletDamage * damageMultiplier, _knockbackDistance, _duration, _maxPenetration, attackableEntityType);
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
        float randomErrorAngle = Random.Range(-_spreadAngle / 2, _spreadAngle / 2);
        return Quaternion.AngleAxis(randomErrorAngle, Vector3.forward) * v;
    }
}
