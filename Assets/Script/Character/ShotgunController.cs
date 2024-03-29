
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;    //UI 클릭시 터치 이벤트 발생 방지.


// Control shotgun and apply item effect
public class ShotgunController : MonoBehaviour
{
    enum AimType
    {
        Auto,
        Touch,
        Mouse
    }
    ShotgunScript _shotgun;

    [SerializeField]
    public Catrige[] _catrigeList;
    public Catrige _currentCatrige;
    public uint _currentCatrigeNumber = 0;
    public Button _fireButton;

    // Radious of auto-aim
    public float _autoAimRadious = 10f;
    // shooting angle
    public float _shotgunAngle = 0;


    public GameObject _shotgunShell;
    Vector3 _shellDropPosition = new(0.96f, 0.18f, 0f);
    bool _canShoot = true;
    bool isAimed = false;
    public PlayerController playerController;

    AimType _aimType; // 0: auto, 1: maunal

    private void Start()
    {
        GameObject shotgunPrefab = Resources.Load<GameObject>("Prefabs/ShotgunPrefab");
        GameObject shotgunObject = Instantiate(shotgunPrefab, Vector3.zero, Quaternion.identity);
        shotgunObject.transform.parent = transform;
        shotgunObject.transform.localScale = new Vector3(1f,1f,1f);
        shotgunObject.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        playerController = FindObjectOfType<PlayerController>();

        // create shotgun member variable
        _shotgun = GameObject.FindObjectOfType<ShotgunScript>();

        // shogtun shell
        _shotgunShell = Resources.Load("Prefabs/shotgun_Shell") as GameObject;

        // joystick
        //_fixedJoystick = GameObject.FindWithTag("GameController").GetComponent<FixedJoystick>();

        _currentCatrige = _catrigeList[0];
        _aimType = AimType.Mouse;
    }

    // Update is called once per frame
    void Update()
    {
        if (_aimType == AimType.Auto)
        {
            isAimed = AutoAimWithoutJoyStick();
        }
        else if (_aimType == AimType.Touch)
            isAimed = ManualAim();
        else if (_aimType == AimType.Mouse)
        {
            isAimed = MouseAim();
        }
        if (_canShoot && isAimed)
        {
            Shoot(isAimed);
        }
    }

    public void FireGun_Catrige()
    {
        _shotgun.Fire(_currentCatrige, playerController._attackDamage);
        GenerateShotgunShell(_shotgunAngle + 180.0f);
        ShakeCamera();
    }
    public void ShakeCamera()
    {
        CameraShake cameraShake = GameObject.FindObjectOfType<CameraShake>();
        cameraShake.Shake(Quaternion.Euler(0, 0, _shotgunAngle) * Vector3.left);
    }

    public void GenerateShotgunShell(float shellEulerAngle)
    {
        GameObject shotgunShell = Instantiate(_shotgunShell, _shotgun.transform, true);
        shotgunShell.transform.localPosition = _shellDropPosition;
        shotgunShell.transform.SetParent(null);
        shotgunShell.GetComponent<ShellScript>().SetDirection(shellEulerAngle);
    }
    bool AutoAimWithoutJoyStick()
    {
            GameObject nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null)
            {
                RotateShotgun(nearestEnemy.transform.position - transform.position);
                return true;
            }
            return false;
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
            // FireGun();
            FireGun_Catrige();
            // cooltime...
            _canShoot = false;
            Invoke("ActiveShoot", playerController.Cooldown());
        }
    }

    public void ActiveShoot()
    {
        _canShoot = true;
    }

    public void changeCatrigeType()
    {
        _currentCatrigeNumber += 1;
        _currentCatrigeNumber %= 3;
        _currentCatrige = _catrigeList[_currentCatrigeNumber];
    }
    public void changeCatrigeTypeReverse()
    {
        _currentCatrigeNumber += 2;
        _currentCatrigeNumber %= 3;
        _currentCatrige = _catrigeList[_currentCatrigeNumber];
    }
    public bool ManualAim()
    {
        if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject() == false)
        {

            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0));
            RotateShotgun(pos - transform.position);
            return true;
        }
        return false;
    }
    public bool MouseAim()
    {
        if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            RotateShotgun(pos - transform.position);
            return true;
        }
        return false;
    }
}
