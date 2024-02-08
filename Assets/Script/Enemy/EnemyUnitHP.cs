using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyUnitHP : Entity
{
    // audio instances
    public AudioSource _hurtSoundSource;
    public AudioClip _hurtSound;
    public AudioSource _deadSoundSource;
    public AudioClip _deadSound;

    void Start()
    {
        // init stat
        _type = Entity.EntityType.Enemy;
        _maxHP = 1;
        _hp = _maxHP;
        _name = "Enemy" + gameObject.GetInstanceID();
        _attackDamage = 1;
        _speed = 1;
        _radious = 0.5f; // if chose circle collider
        _size = new(0.5f, 0.5f, 0f); // if chose box collider


        // init audio
        _hurtSoundSource = gameObject.AddComponent<AudioSource>();
        _hurtSound = Resources.Load<AudioClip>("Audio/dspunch");
        _deadSoundSource = gameObject.AddComponent<AudioSource>();
        _deadSound = Resources.Load<AudioClip>("Audio/dsbgdth1");

        /*
            if enemy has circle collider
        */
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        collider.radius = _radious;
        /*
            if enemy has box collider
        */
        // BoxCollider2D collider = GetComponent<BoxCollider2D>();
        // collider.size = _size;
    } 
    void Update()
    {
        //Debug.Log("enemy: (" + transform.position.x + ", " + transform.position.y + ", " + transform.position.z + ")\n");
        Move();
    }

    private void Move()
    {
        /*
            GetPlayerPosition returns position itself if NO player exists
            This enables enemy auto-stop if player dead
        */
        Vector3 playerPosition = GetPlayerPosition();
        
        if ((transform.position - playerPosition).magnitude > 1) {
            //transform.Translate((playerPosition - transform.position).normalized * 0.005f);
            transform.Translate((playerPosition - transform.position).normalized * Time.deltaTime);
            //transform.Translate((playerPosition - transform.position).normalized * 0.005f);
        } 
    }
    public void DecreaseHP(float delta)
    {
        /*
            Be careful that _DecreaseHP includes
            Destroy(this.gameObject);
        */
        _DecreaseHP(delta);
        if (IsDead())
        { // dead
            if(_deadSound != null)
            {
                _deadSoundSource.PlayOneShot(_deadSound);
            }
        }
        else
        { // alive
            if (_hurtSound != null)
            {
                _hurtSoundSource.PlayOneShot(_hurtSound);
            }
        }
        return;
    }
    private Vector3 GetPlayerPosition()
    {
        GameObject player = GameObject.Find("Main Character");
        // GetComponent<Transform>();
        if (player == null)
        {
            return transform.position;
        }
        else
        {
            return player.transform.position;
        }
    }
}

