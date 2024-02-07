using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Control shotgun and apply item effect
public class ShotgunController : MonoBehaviour
{
    ShotgunScript _shotgun;
    BulletScript _bullet;
    GameObject _bulletPrefab;

    public FixedJoystick _fixedJoystick;
    public Button _fireButton;

    // Radious of auto-aim
    float _autoAimRadious = 10f;
    // 산탄총 발사 각도
    public float _shotgunAngle = 0;


    public GameObject _shotgunShell;
    Vector3 _shellDropPosition = new(4.81f, 0.88f, 0f);

    // Start is called before the first frame update
    private void Start()
    {
        // CreateShotGun();
        GameObject shotgunPrefab = Resources.Load<GameObject>("Prefabs/ShotgunPrefab");
        GameObject shotgunObject = Instantiate(shotgunPrefab, Vector3.zero, Quaternion.identity);
        shotgunObject.transform.parent = transform;

        // bullet
        _bulletPrefab = Resources.Load("Prefabs/bullet") as GameObject;

        // create bullet member variable
        _bullet = gameObject.AddComponent<BulletScript>();
        // create shotgun member variable
        _shotgun = gameObject.AddComponent<ShotgunScript>();
        _shotgun.transform.SetParent(null);

        // shogtun shell
        _shotgunShell = Resources.Load("Prefabs/shotgun_Shell") as GameObject;

        // joystick
        _fixedJoystick = GameObject.FindWithTag("GameController").GetComponent<FixedJoystick>();

        // fire button manage
        _fireButton = GameObject.Find("Fire Button").GetComponent<Button>();
        // subscribe to the onClick event
        _fireButton.onClick.AddListener(FireGun);

        // 자동사격: 적을 찾아서 총을 발사하는 함수를 1초마다 호출
        InvokeRepeating("AutoShoot", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        AutoAim();
    }

    // private void CreateShotGun(){
    //     GameObject shotgunPrefab = Resources.Load<GameObject>("Prefabs/ShotgunPrefab");
    //     GameObject shotgunObject = Instantiate(shotgunPrefab, Vector3.zero, Quaternion.identity);
    //     shotgunObject.transform.parent = transform;
    // }

    // 총 쏘는 함수
    public void FireGun()
    {
        _shotgun.Fire(_shotgunAngle, _bullet, _bulletPrefab);
        _shotgun.PlayFireSound();
        GenerateShotgunShell();
        ShakeCamera();
    }
    public void ShakeCamera()
    {
        CameraShake cameraShake = GameObject.FindObjectOfType<CameraShake>();
        cameraShake.Shake(Quaternion.Euler(0, 0, _shotgunAngle) * Vector3.left);
    }

    public void GenerateShotgunShell()
    {
        //GameObject shotgunShell = Instantiate(_shotgunShell, _shellDropPosition,Quaternion.identity, transform);
        GameObject shotgunShell = Instantiate(_shotgunShell, transform, true);
        shotgunShell.transform.localPosition = _shellDropPosition;
        shotgunShell.transform.SetParent(null);
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
            _shotgun.transform.rotation = Quaternion.Euler(0, 0, _shotgunAngle);
        }
    }

    float Vec2Angle(Vector3 v)
    {
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    // 자동 사격 함수
    void AutoShoot()
    {
        GameObject nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null)
        {
            FireGun();
            // ShootShotgun();
            // GenerateShotgunShell();
            // Debug.Log("근처 적" + nearestEnemy);
            // Debug.Log("자동 사격");
        }
    }
}
