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

    public bool _isInvincible = false;

    // heal variable
    public float _healPerSec = 0.05f;
    public float _healPeriod = 1f;

    // 공격 관련 변수
    public float _speedMod = 0f;
    public float _attackCooldown = 0f;

    // audio instances
    public AudioSource _hurtSoundSource;
    public AudioClip _hurtSound;
    public AudioSource _deadSoundSource;
    public AudioClip _deadSound;
    public float _invincibleTime;

    // player collision with the enemy
    private bool isTouchingEnemy = false;

    void Start()
    {

        _hp = _maxHP;
        _name = "Player" + gameObject.GetInstanceID();
        CreateCursor();
        _cursor = _cursorObject.GetComponent<Cursor>();
        _shotGun = GetComponentInChildren<ShotgunController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // init audio
        _hurtSoundSource = gameObject.AddComponent<AudioSource>();
        _hurtSound = Resources.Load<AudioClip>("Audio/dsoof");
        _deadSoundSource = gameObject.AddComponent<AudioSource>();
        _deadSound = Resources.Load<AudioClip>("Audio/dsplpain");

        // basic self healing
        InvokeRepeating("Regenerate", 0f, _healPeriod);
    }


    void Update()
    {
        //주인공(커서) 움직이기
        MovePlayerWithCursor_modified();
    }

    private void CreateCursor()
    {
        if (_cursorObject == null)
        {
            GameObject cursorPrefab = Resources.Load<GameObject>("Prefabs/CursorPrefab");
            _cursorObject = Instantiate(cursorPrefab, Vector3.zero, Quaternion.identity);
            _cursorObject.transform.parent = transform;
        }
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
    public override void DecreaseHP(float damage = 1.0f)
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
            return;
        }

        // 무적
        Unbeatable();
        Invoke("Unbeatable", _invincibleTime);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        bool local_debug = false;
        int n_attack = local_debug ? 2 : 1;
        if (other.gameObject.CompareTag("Enemy"))
        {
            isTouchingEnemy = true;
        }

        else
        {
            // test code for the function operates well
            if (_hp > 0 && local_debug)
            {
                other.gameObject.transform.position = new Vector3(10, 10, 0);
            }
        }
        StartCoroutine(DamageRoutine(other.gameObject.GetComponent<Entity>()._attackDamage, other.gameObject.GetComponent<Entity>()._attackSpeed));
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            isTouchingEnemy = false;
        }
    }


    IEnumerator DamageRoutine(float damage, float period)
    {
        while (_hp > 0 && isTouchingEnemy == true)
        {
            // give damage
            DecreaseHP(damage);

            // wait
            yield return new WaitForSeconds(period);
        }
    }

    void Regenerate()
    {
        _IncreaseHP(_healPerSec);
    }

    // player hp restoration function
    public void SetHealthPoint()
    {
        if (_hp < _maxHP)
        {
            _hp += 1;
        }
    }

    public void SetAttackDamage()
    {
        _attackDamage += 0.1f;
    }

    public void SetAttackSpeed()
    {
        _speedMod += 0.1f;
    }

    // 공격 주기 함수
    public float Cooldown()
    {
        _attackCooldown = 1 / (_attackSpeed * (1 + _speedMod));
        return _attackCooldown;
    }

    /*
        UI를 위한 반환 함수들
    */
    // Hp 반환 함수
    public float HealthPointManager()
    {
        return _hp;
    }
    // 공격력 반환 함수
    public float AttackDamageManager()
    {
        return _attackDamage;
    }
    // 공격속도 반환 함수
    public float AttackSpeedManager()
    {
        float _speed = _attackSpeed * (1 + _speedMod);
        return _speed;
    }
}
