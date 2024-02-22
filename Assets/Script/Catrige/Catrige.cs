using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catrige : MonoBehaviour
{
    //차후 1번의 공격에 여러 탄환이 섞일경우, 아래의 내용을 리스트로 만들어야 할 수 있다.
    public BulletScript.BulletType _bulletType;
    public float _bulletDamage;
    public float _bulletSpeed;
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
    public void FireCatrige(Vector3 lineOfFire, Entity.EntityType attackableEntityType)
    {
        //총알이 나아갈 방향을 계산한다. 
        //총알 갯수만큼 총알이 움직여야할 방향으로 generateBullet를 호출함.
        //weapon fire 관련 함수 참조
    }

    //총알을 생성하는 함수이다. 
    protected void generateBullet(Vector3 direction, Entity.EntityType attackableEntityType)
    {
        //TODO
        //총알 프리팹을 생성한다. instantiate
        //총알에 속도와 방향을 지정한다.
        //weapon fire 관련 함수 참조
    } 
    
    protected void GetBulletPrefab(BulletScript.BulletType _bulletType)
    {
        //start에서 호출하는 함수이다.
        //bullet prefab를 받아와 저장해둔다.
    }


}
