using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyUnitBase : Entity
{
    // audio instances
    public AudioSource _hurtSoundSource;
    public AudioClip _hurtSound;
    public AudioSource _deadSoundSource;
    public AudioClip _deadSound;

    // enemy size
    public float _enemyRadious = 0.5f; // if chose circle collider
    public Vector3 _enemySize = new(0.5f, 0.5f, 0f); // if chose box collider

    void Start()
    {
        // init stat
        _stat = gameObject.AddComponent<Entity>();
        _stat.SetType(Entity.EntityType.Enemy);
        _stat.SetHP(1);
        // _stat.SetSize();
        // _stat.SetPosition();
        // _stat.SetSpeed();
        // _stat.SetAttackDamage();
        // _stat.SetInventorySize();


        // init audio
        _hurtSoundSource = gameObject.AddComponent<AudioSource>();
        _hurtSound = Resources.Load<AudioClip>("Audio/dspunch");
        _deadSoundSource = gameObject.AddComponent<AudioSource>();
        _deadSound = Resources.Load<AudioClip>("Audio/dsbgdth1");

        /*
            if enemy has circle collider
        */
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        collider.radius = _enemyRadious;
        /*
            if enemy has box collider
        */
        // BoxCollider2D collider = GetComponent<BoxCollider2D>();
        // collider.size = _enemySize;
    } 
    void Update()
    {
        //Debug.Log("enemy: (" + transform.position.x + ", " + transform.position.y + ", " + transform.position.z + ")\n");
        Move();
    }

    private void Move()
    {
        Vector3 playerPosition = GetPlayerPosition(); 
        
        if ((transform.position - playerPosition).magnitude > 1) {
            //transform.Translate((playerPosition - transform.position).normalized * 0.005f);
            transform.Translate((playerPosition - transform.position).normalized * Time.deltaTime);
            //transform.Translate((playerPosition - transform.position).normalized * 0.005f);
        } 
    }
    public void DecreaseHP()
    {
        _stat.DecreaseHP(1);
        if (_stat.IsDead())
        { // dead
            if(_deadSound != null)
            {
                _deadSoundSource.PlayOneShot(_deadSound);
            }
            Destroy(this.gameObject);
        }
        else
        { // alive
            Debug.Log(_stat._hp);
            if (_hurtSound != null)
            {
                _hurtSoundSource.PlayOneShot(_hurtSound);
            }
        }
        return;
    }
    private Vector3 GetPlayerPosition()
    {
        Transform playerInfo = GameObject.Find("Main Character").GetComponent<Transform>();
        return playerInfo.position;
    }
}

