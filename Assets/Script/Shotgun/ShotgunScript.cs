using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShotgunScript : MonoBehaviour
{
    public FixedJoystick _fixedJoystick;
    public Button _fireButton;

    public GameObject _shotgunShell;
    public GameObject _bullet;
    Vector3 _shellDropPosition = new(4.81f, 0.88f, 0f);

    private AudioSource _audioSource;
    public AudioClip _gunshotSound;


    /*
        산탄총 발사 관련 변수
    */
    // 총구 위치
    public Vector3 _firePoint = new(1.6f, 0f, 0f);
    // 산탄총 발사 각도
    public float _shotgunAngle = 0;
    // Spreading angle
    public float _spreadAngle = 15f;
    // 총알 개수
    public int _pelletCount = 8;
    // 산탄 투사체 속도 계수
    public float _pelletSpeed = 1f;
    // Radious of auto-aim
    float _autoAimRadious = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _shotgunShell = Resources.Load("Prefabs/_shotgunShell") as GameObject;
        _bullet = Resources.Load("Prefabs/bullet") as GameObject;
        //_fixedJoystick 을 실시간으로 찾아오는 스크립트 작성
        _fixedJoystick = GameObject.FindWithTag("GameController").GetComponent<FixedJoystick>();

        //
        _audioSource = gameObject.AddComponent<AudioSource>();
        _gunshotSound = Resources.Load<AudioClip>("Audio/_gunshotSound");

        //fire button manage
        _fireButton = GameObject.Find("Fire Button").GetComponent<Button>();
        _fireButton.onClick.AddListener(CustomButton_onClick); //subscribe to the onClick event

        // 자동사격: 적을 찾아서 총을 발사하는 함수를 1초마다 호출
        InvokeRepeating("AutoShoot", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        AutoAim();
        // transform.LookAt(angle);

        /*
        산탄총 발사
        if(Input.GetKeyDown(KeyCode.Space))
        {
            FindEnemiesAndShoot();
        }
        */
    }

    public void GenerateShotgunShell()
    {
        //GameObject shotgunShell = Instantiate(_shotgunShell, _shellDropPosition,Quaternion.identity, transform);
        GameObject shotgunShell = Instantiate(_shotgunShell, transform, true);
        shotgunShell.transform.localPosition = _shellDropPosition;
        shotgunShell.transform.SetParent(null);
    }

    public void MakeFireSound()
    {
        if (_gunshotSound != null)
        {
            _audioSource.PlayOneShot(_gunshotSound);
        }
    }


    void Awake()
    {

    }

    //Handle the onClick event
    void CustomButton_onClick()
    {
        //Debug.Log("testtttt");
        ShootShotgun();
        GenerateShotgunShell();
        MakeFireSound();
    }


    /*
        산탄총을 발사하는 데 필요한 함수들
    */
    // 가까운 적을 찾는 함수
    GameObject FindNearestEnemy()
    {
        // // Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 10f, targetLayer);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _autoAimRadious, LayerMask.GetMask("Enemy"));

        if (enemies == null)
        { // killed every enemy || no enemy detected
            return null;
        }

        GameObject nearestEnemy = null;
        float minDistance = float.MaxValue;
        foreach (Collider2D enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < minDistance)
            {
                nearestEnemy = enemy.gameObject;
                minDistance = distanceToEnemy;
            }
        }

        return nearestEnemy;
    }

    // 산탄총 발사 함수
    void ShootShotgun()
    {
        for (int i = 0; i < _pelletCount; i++)
        {
            float randomErrorAngle = UnityEngine.Random.Range(-_spreadAngle / 2, _spreadAngle / 2);
            Vector3 firePoint = Quaternion.AngleAxis(_shotgunAngle, Vector3.forward) * _firePoint;
            GameObject pellet = Instantiate(_bullet, transform.position + firePoint, Quaternion.identity);
            pellet.GetComponent<BulletScript>().SetVelocity(_pelletSpeed, _shotgunAngle + randomErrorAngle);
        }
    }

    // 자동 사격 함수
    void AutoShoot()
    {
        GameObject nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null)
        {
            ShootShotgun();
            GenerateShotgunShell();
            Debug.Log("근처 적" + nearestEnemy);
            Debug.Log("자동 사격");
        }
    }

    float Vec2Angle(Vector3 v)
    {
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    /*
        if v is zero vector -> rotate shotgun along v
        else                -> do nothing
    */
    void RotateShotgun(Vector3 v)
    {
        if (v != Vector3.zero)
        {
            _shotgunAngle = Vec2Angle(v);
            if (-90 < _shotgunAngle && _shotgunAngle < 90)
            { // Do not flip shotgun image
                transform.localScale = new(0.2f, 0.2f, 1);
            }
            else
            { // Flip shotgun image
                transform.localScale = new(0.2f, -0.2f, 1);
            }
            transform.rotation = Quaternion.Euler(0, 0, _shotgunAngle);
        }
    }

    void AutoAim()
    {
        // Not holding joystick
        if (_fixedJoystick.Direction == Vector2.zero)
        {
            GameObject nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null)
            {
                RotateShotgun(nearestEnemy.transform.position - transform.position);
            }
        }
        else
        {
            RotateShotgun(_fixedJoystick.Direction);
        }
    }
}
