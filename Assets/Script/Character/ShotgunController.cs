
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Control shotgun and apply item effect
public class ShotgunController : MonoBehaviour
{
    ShotgunScript _shotgun;
    BulletScript _bullet;

    [SerializeField]
    public Catrige[] _catrigeList;
    public Catrige _currentCatrige;
    public uint _currentCatrigeNumber = 0;
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
        ChangeBulletType(BulletScript.BulletType.PlayerSlug);

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

        _currentCatrige = _catrigeList[0];
    }

    // Update is called once per frame
    void Update()
    {
        AutoAim();
        if (_canShoot)
        {
            AutoShoot();
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

    public void FireGun_Catrige()
    {
        // _shotgun.Fire(_shotgunAngle);
        _shotgun.Fire(_shotgunAngle, _currentCatrige);
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

    // autoshooting function
    void AutoShoot()
    {
        GameObject nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null)
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
            case BulletScript.BulletType.PlayerSlug:
                _bullet = GameObject.FindObjectOfType<BulletSlug>();
                break;
            case BulletScript.BulletType.PlayerScatter:
                _bullet = GameObject.FindObjectOfType<BulletScatter>();
                break;
            case BulletScript.BulletType.PlayerRocket:
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
            case BulletScript.BulletType.PlayerScatter:
                _bulletType = BulletScript.BulletType.PlayerSlug;
                break;
            case BulletScript.BulletType.PlayerSlug:
                _bulletType = BulletScript.BulletType.PlayerRocket;
                break;
            case BulletScript.BulletType.PlayerRocket:
                _bulletType = BulletScript.BulletType.PlayerScatter;
                break;
            default:
                _bulletType = BulletScript.BulletType.PlayerScatter;
                break;
        }
        ChangeBulletType(_bulletType);
    }

    public void changeCatrigeType()
    {
        _currentCatrigeNumber += 1;
        _currentCatrigeNumber %= 3;
        _currentCatrige = _catrigeList[_currentCatrigeNumber];
    }
}
