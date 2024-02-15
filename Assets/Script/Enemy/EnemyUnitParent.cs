using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitParent : Entity
{
    [Header("Knockback")]
    public Vector3 _knockbackVelocity;
    [Range(0f, 5f)]
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
        UpdateKnockbackVelocity();
        transform.Translate(_knockbackVelocity * Time.deltaTime);

        Vector3 displacement = GetPlayerReletivePosition();
        if (displacement.magnitude > 1)
        {
            Vector3 velocity = _speed * displacement.normalized;
            transform.Translate(velocity * Time.deltaTime);
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
        _knockbackVelocity += knockbackDistance * dir.normalized;
    }

    private void UpdateKnockbackVelocity()
    {
        if (_knockbackVelocity != Vector3.zero)
        {
            float knockbackSpeed = _knockbackVelocity.magnitude - _frictionCoeff;
            if (0 < knockbackSpeed)
            {
                _knockbackVelocity = knockbackSpeed * _knockbackVelocity.normalized;
            }
            else
            {
                _knockbackVelocity = Vector3.zero;
            }
        }
    }
}
