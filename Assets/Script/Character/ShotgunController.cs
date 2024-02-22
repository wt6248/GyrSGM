
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Control shotgun and apply item effect
public class ShotgunController : MonoBehaviour
{
    ShotgunScript _shotgun;
    BulletScript _bullet;
    /*
        bulletType for player speeds each respects to weight of weapon
    */
    BulletScript.BulletType _bulletType;
    // GameObject _bulletPrefab;

    public FixedJoystick _fixedJoystick;
    public Button _fireButton;

    // Radious of auto-aim
    public float _autoAimRadious = 10f;
    // shooting angle
    public float _shotgunAngle = 0;


    public GameObject _shotgunShell;
    Vector3 _shellDropPosition = new(0.96f, 0.18f, 0f);
    bool _canShoot = false;
    public PlayerController playerController;

    int _aimType = 0; // 0: auto, 1: maunal
    private void Start()
    {
        // TODO? : instantiate prefab at the start()
        GameObject shotgunPrefab = Resources.Load<GameObject>("Prefabs/ShotgunPrefab");
        GameObject shotgunObject = Instantiate(shotgunPrefab, Vector3.zero, Quaternion.identity);
        shotgunObject.transform.parent = transform;
        playerController = FindObjectOfType<PlayerController>();

        // bullet
        // _bulletPrefab = Resources.Load("Prefabs/bullet") as GameObject;

        // set bullet type and change bullet
        //ChangeBulletType(BulletScript.BulletType.Rocket);
        //ChangeBulletType(BulletScript.BulletType.Scatter);
        ChangeBulletType(BulletScript.BulletType.Slug);

        // create shotgun member variable
        _shotgun = GameObject.FindObjectOfType<ShotgunScript>();

        // shogtun shell
        _shotgunShell = Resources.Load("Prefabs/shotgun_Shell") as GameObject;

        // joystick
        _fixedJoystick = GameObject.FindWithTag("GameController").GetComponent<FixedJoystick>();

        // fire button manage
        _fireButton = GameObject.Find("Fire Button").GetComponent<Button>();
        // subscribe to the onClick event
        _fireButton.onClick.AddListener(FireGun);

        // autoshooting function
        StartCoroutine(AutoShootCooldown());
        //InvokeRepeating("AutoShoot", 0f, 1.5f);
        _aimType = 1;
    }

    // Update is called once per frame
    void Update()
    {
        bool isAimed = false;
        if (_aimType == 0) {
            isAimed = AutoAim();
        }
            
            
        else if (_aimType == 1)
            isAimed = ManualAim();
        if(_canShoot && isAimed) 
        {
            Shoot(isAimed);
        }
    }

    // shooting function
    public void FireGun()
    {
        // _shotgun.Fire(_shotgunAngle);
        _shotgun.Fire(_shotgunAngle, _bullet);
        GenerateShotgunShell(_shotgunAngle + 180.0f);
        ShakeCamera();
    }
    public void ShakeCamera()
    {
        CameraShake cameraShake = GameObject.FindObjectOfType<CameraShake>();
        cameraShake.Shake(Quaternion.Euler(0, 0, _shotgunAngle) * Vector3.left);
    }

    public void GenerateShotgunShell()
    {
        GameObject shotgunShell = Instantiate(_shotgunShell, _shotgun.transform, true);
        shotgunShell.transform.localPosition = _shellDropPosition;
        shotgunShell.transform.SetParent(null);
    }
    public void GenerateShotgunShell(float shellEulerAngle)
    {
        GameObject shotgunShell = Instantiate(_shotgunShell, _shotgun.transform, true);
        shotgunShell.transform.localPosition = _shellDropPosition;
        shotgunShell.transform.SetParent(null);
        shotgunShell.GetComponent<ShellScript>().SetDirection(shellEulerAngle);
    }

    bool AutoAim()
    {
        // Not holding joystick
        if (_fixedJoystick.Direction == Vector2.zero)
        {
            GameObject nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null)
            {
                RotateShotgun(nearestEnemy.transform.position - transform.position);
                return true;
            }
            return false;
        }
        else
        {
            RotateShotgun(_fixedJoystick.Direction);
            return true;
        }
    }

    // finding nearest enemy for autoshooting
    GameObject FindNearestEnemy()
    {
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
                _shotgun.transform.localScale = new(1f, 1f, 1);
            }
            else
            { // Flip shotgun image
                _shotgun.transform.localScale = new(1f, -1f, 1);
            }
            _shotgun.transform.rotation = Quaternion.Euler(0, 0, _shotgunAngle);
        }
    }

    float Vec2Angle(Vector3 v)
    {
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    // shooting function
    void Shoot(bool isAimed)
    {
        GameObject nearestEnemy = FindNearestEnemy();
        if (isAimed)
        {
            FireGun();
            _canShoot = false; 
        }
    }
    IEnumerator AutoShootCooldown()
    {
        while (true)
        {            
            yield return new WaitForSeconds(playerController.Cooldown());
            _canShoot = true;
        }
    }

    public void ChangeBulletType(BulletScript.BulletType bulletType)
    {
        _bulletType = bulletType;
        switch (bulletType)
        {
            case BulletScript.BulletType.Slug:
                _bullet = GameObject.FindObjectOfType<BulletSlug>();
                break;
            case BulletScript.BulletType.Scatter:
                _bullet = GameObject.FindObjectOfType<BulletScatter>();
                break;
            case BulletScript.BulletType.Rocket:
                _bullet = GameObject.FindObjectOfType<BulletRocket>();
                break;
            default:
                _bullet = GameObject.FindObjectOfType<BulletRocket>();
                break;
        }
    }

    public void ChangeBulletTypeByButton()
    {
        switch (_bulletType)
        {
            case BulletScript.BulletType.Scatter:
                _bulletType = BulletScript.BulletType.Slug;
                break;
            case BulletScript.BulletType.Slug:
                _bulletType = BulletScript.BulletType.Rocket;
                break;
            case BulletScript.BulletType.Rocket:
                _bulletType = BulletScript.BulletType.Scatter;
                break;
            default:
                _bulletType = BulletScript.BulletType.Scatter;
                break;
        }
        ChangeBulletType(_bulletType);
    }

    public bool ManualAim() {
        
        if (Input.touchCount > 0) {
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0));
            RotateShotgun(pos - transform.position);
            return true;
        }
        return false;
    }
}
