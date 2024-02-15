using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitParent : Entity
{
    [Header("Knockback")]
    public Vector3 _knockbackMomentum;
    public float _frictionCoeff = 0.4f;

    [Header("Audio Instances")]
    public AudioSource _hurtSoundSource;
    public AudioClip _hurtSound;
    public AudioSource _deadSoundSource;
    public AudioClip _deadSound;

    void Start()
    {

    }

    void Update()
    {

    }

    protected void Move()
    {
        Vector3 displacement = GetPlayerReletivePosition();

        if (displacement.magnitude > 1)
        {
            Vector3 translation = _speed * Time.deltaTime * displacement.normalized;
            // consider knockback
            if (_knockbackMomentum != Vector3.zero)
            {
                float knockbackSpeed = _knockbackMomentum.magnitude - _frictionCoeff;
                if (knockbackSpeed > 0)
                {
                    _knockbackMomentum = knockbackSpeed * _knockbackMomentum.normalized;
                }
                else
                {
                    _knockbackMomentum = Vector3.zero;
                }
                translation += _knockbackMomentum;
            }
            transform.Translate(translation);
        }
    }
    override public void DecreaseHP(float delta)
    {
        /*
            Be careful that _DecreaseHP includes
            Destroy(this.gameObject);
        */
        _DecreaseHP(delta);
        if (IsDead())
        { // dead
            if (_deadSound != null)
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

    /*
        GetPlayerReletivePosition returns Vector3.zero if NO player exists
        This enables enemy auto-stop if player dead
    */
    private Vector3 GetPlayerReletivePosition()
    {
        GameObject player = GameObject.Find("Main Character");
        // GetComponent<Transform>();
        if (player == null)
        {
            return Vector3.zero;
        }
        else
        {
            return player.transform.position - transform.position;
        }
    }

    public void Knockback(Vector3 dir, float knockbackDistance)
    {
        _knockbackMomentum = knockbackDistance * dir.normalized;
    }

    bool IsSameDirection(Vector3 a, Vector3 b)
    {
        return a.normalized == b.normalized;
    }
}
