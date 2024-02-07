using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerController : Entity
{
    private Cursor _cursor;
    private ShotgunController _shotGun;
    private GameObject _cursorObject;
    private SpriteRenderer _spriteRenderer;
    public GyroGameObj _gyroControl;

    private bool _isInvincible = false;

    // heal variable
    private float _healPerSec = 0.05f;
    private float _healPeriod = 1f;


    // audio instances
    public AudioSource _hurtSoundSource;
    public AudioClip _hurtSound;
    public AudioSource _deadSoundSource;
    public AudioClip _deadSound;

    // player size
    public const float playerRadious = 0.5f; // if chose circle collider
    public readonly Vector2 playerSize = new(0.5f, 0.5f); // if chose box collider

    void Start()
    {
        // init stat
        _type = Entity.EntityType.Player;
        _maxHP = 5;
        _hp = _maxHP;
        _name = "Player";
        _attackDamage = 1;
        _speed = 4;
        _radious = 0.5f; // if chose circle collider
        _size = new(0.5f, 0.5f, 0f); // if chose box collider


        //_cursor = GetComponentInChildren<Cursor>();
        CreateCursor();
        _cursor = _cursorObject.GetComponent<Cursor>();
        _shotGun = GetComponentInChildren<ShotgunController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //_gyroControl = GameObject.Find("Gyro Controller").GetComponent<GyroGameObj>();


        _hurtSoundSource = gameObject.AddComponent<AudioSource>();
        _hurtSound = Resources.Load<AudioClip>("Audio/dsoof");
        _deadSoundSource = gameObject.AddComponent<AudioSource>();
        _deadSound = Resources.Load<AudioClip>("Audio/dsplpain");

        /*
            if player has circle collider
        */
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        collider.radius = playerRadious;
        /*
            if player has box collider
        */
        // BoxCollider2D collider = GetComponent<BoxCollider2D>();
        // collider.size = playerSize;

        // basic self healing
        InvokeRepeating("Regenerate", 0f, _healPeriod);
    }


    void Update()
    {
        //주인공(커서) 움직이기
        //MovePlayerWithCursor();
        MovePlayerWithCursor_modified();

        //임시로 주인공 자이로 직접 움직이기
        //tempMovePlayerByGyro();

        //총 발사
        if (Input.GetKeyDown(KeyCode.Space))
        { //스페이스바를 누르면 발사
            _shotGun.FireGun();
        }

    }

    private void CreateCursor()
    {
        Debug.Log("CreateCursor() 메서드 호출됨");
        if (_cursorObject == null)
        {
            GameObject cursorPrefab = Resources.Load<GameObject>("Prefabs/CursorPrefab");
            _cursorObject = Instantiate(cursorPrefab, Vector3.zero, Quaternion.identity);
            _cursorObject.transform.parent = transform;
            Debug.Log("커서 생성");
        }
    }



    void MovePlayerWithCursor()
    {
        // 커서와 플레이어 간의 상대적인 위치 계산
        Vector3 relativePosition = _cursor.CursorPosition() - transform.position;

        // 플레이어와 커서 간의 거리 계산
        float distance = relativePosition.magnitude;

        // 플레이어의 이동 속도를 거리에 따라 조절 (임의로 0.01f로 설정)
        float speed = distance * 0.01f;

        // 상대적인 위치를 정규화하여 이동 방향을 설정하고 속도를 적용
        Vector3 moveDirection = relativePosition.normalized * speed;

        // 플레이어 이동
        transform.position += moveDirection;
    }

    void MovePlayerWithCursor_modified()
    {
        if (_cursor == null)
        {
            _cursor = transform.GetChild(1).gameObject.GetComponent<Cursor>();
        }
        Vector3 relativePosition = _cursor.CursorLocalPosition();
        transform.Translate(relativePosition * Time.deltaTime * _speed);
    }

    void tempMovePlayerByGyro()
    {
        Vector3 a = _gyroControl.GetGyroValue();
        //Debug.Log(a);
        Vector3 b = new Vector3(a.x, a.y, 0f) * Time.deltaTime * 20;
        //Debug.Log(b);
        transform.Translate(b);
    }

    // void FireGun()
    // {
    //     _shotGun.FireGun(transform.position);
    // }
    private void Unbeatable()
    {
        _isInvincible = !_isInvincible;

        if (_isInvincible)
        {
            _spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            _spriteRenderer.color = new Color(1, 1, 1, 1);
        }
    }
    void GetDamaged(float damage = 1.0f, float invincibleTime = 0.5f)
    {
        if (_isInvincible)
        {
            return;
        }

        _DecreaseHP(damage);
        if (_hurtSound != null && !IsDead())
        {
            _hurtSoundSource.PlayOneShot(_hurtSound);
        }
        Debug.Log("Player HP is " + _hp);
        if (IsDead())
        {
            if (_deadSound != null)
            {
                _deadSoundSource.PlayOneShot(_deadSound);
            }
            Debug.Log("Player Died");
            CancelInvoke("Regenerate");

            /*
                Enemy finds player object -> do not destroy player
            */
            CancelInvoke("AutoShoot");
            Time.timeScale = 0;
            Button FireButton = GameObject.Find("Fire Button").GetComponent<Button>();
            FireButton.onClick.RemoveAllListeners();
            // Destroy(this.gameObject);
            return;
        }

        // 무적
        Unbeatable();
        Invoke("Unbeatable", invincibleTime);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        bool local_debug = false;
        int n_attack = local_debug ? 2 : 1;

        StartCoroutine(DamageRoutine(1f, 0.1f));


        // test code for the function operates well
        if (_hp > 0 && local_debug)
        {
            other.gameObject.transform.position = new Vector3(10, 10, 0);
        }
    }


    IEnumerator DamageRoutine(float damage, float period)
    {
        while (_hp > 0)
        {
            // give damage
            GetDamaged(damage);

            // wait
            yield return new WaitForSeconds(period);
        }
    }

    void Regenerate()
    {
        _IncreaseHP(_healPerSec);
    }
}
