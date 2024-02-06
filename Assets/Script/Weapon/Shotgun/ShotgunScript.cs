using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShotgunScript : Weapon
{
    public FixedJoystick _fixedJoystick;
    public Button _fireButton;

    public GameObject _shotgunShell;
    public GameObject _bullet;
    Vector3 _shellDropPosition = new(4.81f, 0.88f, 0f);


    /*
        산탄총 발사 관련 변수
    */
    // 총구 위치
    public Vector3 _firePoint = new(1.6f, 0f, 0f);

    // 총알 개수
    public int _pelletCount = 8;
    // 산탄 투사체 속도 계수
    public float _pelletSpeed = 1f;


    // Start is called before the first frame update
    void Start()
    {
        /*
            Audio can differ depending on the type of weapon
        */
        _audioSource = gameObject.AddComponent<AudioSource>();
        _gunshotSound = Resources.Load<AudioClip>("Audio/gunshotSound");

        _shotgunShell = Resources.Load("Prefabs/shotgun_Shell") as GameObject;
        _bullet = Resources.Load("Prefabs/bullet") as GameObject;
        //_fixedJoystick 을 실시간으로 찾아오는 스크립트 작성
        _fixedJoystick = GameObject.FindWithTag("GameController").GetComponent<FixedJoystick>();

        //
        _audioSource = gameObject.AddComponent<AudioSource>();
        _gunshotSound = Resources.Load<AudioClip>("Audio/gunshotSound");

        //fire button manage
        _fireButton = GameObject.Find("Fire Button").GetComponent<Button>();
        _fireButton.onClick.AddListener(CustomButton_onClick); //subscribe to the onClick event

        // 자동사격: 적을 찾아서 총을 발사하는 함수를 1초마다 호출
        InvokeRepeating("AutoShoot", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
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

        CameraShake cameraShake = GameObject.FindObjectOfType<CameraShake>();
        cameraShake.Shake(Quaternion.Euler(0, 0, _shotgunAngle) * Vector3.left);
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





}
