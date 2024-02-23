using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // number of bullet in one magazine
    public uint _remainingAmmo = 4;
    public uint _remainingMagazine = 1;
    // capacity of one magazine
    public uint _magazineCapacity = 4;

    public float _reloadTime = 3;
    public float _reloadCoolDown = 0;

    public AudioSource _audioSource = null;
    public AudioClip _gunshotSound = null;

    private Vector3 _firePoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsReloading())
        {
            _reloadCoolDown -= Time.deltaTime;
        }
        else
        {
            _reloadCoolDown = 0;
        }
    }

    private bool IsMagazineEmpty()
    {
        return _remainingAmmo == 0;
    }
    private bool IsReloading()
    {
        return 0 < _reloadCoolDown;
    }
    private bool IsReadyToFire()
    {
        return !IsReloading() || !IsMagazineEmpty();
    }
    public void Fire(float angle, BulletScript bullet)
    {
        if (!IsReadyToFire())
        {
            return;
        }

        for (int i = 0; i < bullet._pelletCount; i++)
        {
            float randomErrorAngle = Random.Range(-bullet._spreadAngle / 2, bullet._spreadAngle / 2);
            Vector3 firePoint = Quaternion.AngleAxis(angle, Vector3.forward) * _firePoint;
            GameObject pellet = Instantiate(bullet._bulletPrefab, transform.position + firePoint, Quaternion.identity);
            pellet.GetComponent<BulletScript>().SetVelocity(bullet._speed, angle + randomErrorAngle);
            pellet.GetComponent<BulletScript>().Activate(Entity.EntityType.Enemy);

            PlayerController player = GameObject.FindObjectOfType<PlayerController>();
            pellet.GetComponent<BulletScript>()._damage = player._attackDamage;
            // pellet.GetComponent<BulletScript>().RecordPenetration(player);
        }

        _remainingAmmo -= 1;
        PlayFireSound();
    }

    // public void Fire(float eulerAngle, Catrige catrige, Entity.EntityType attackableEntityType = Entity.EntityType.Enemy)
    // {
    //     if (!IsReadyToFire())
    //     {
    //         return;
    //     }
    //     float directionX = Mathf.Cos(eulerAngle * Mathf.Deg2Rad);
    //     float directionY = Mathf.Sin(eulerAngle * Mathf.Deg2Rad);
    //     Vector3 FireLine = new(directionX, directionY, 0f);
    //     Vector3 firePoint = Quaternion.AngleAxis(eulerAngle, Vector3.forward) * _firePoint;
    //     Vector3 bulletGenerationWorldPosition = transform.position + firePoint;

    //     catrige.FireCatrige(FireLine, attackableEntityType, bulletGenerationWorldPosition);

    //     _remainingAmmo -= 1;
    //     PlayFireSound();
    // }

    // public void Fire(Vector3 FireLine, Catrige catrige, Entity.EntityType attackableEntityType = Entity.EntityType.Enemy)
    // {
    //     if (!IsReadyToFire())
    //     {
    //         return;
    //     }
    //     Vector3 firePoint = Quaternion.AngleAxis(transform.rotation.eulerAngles.z, Vector3.forward) * _firePoint;
    //     Vector3 bulletGenerationPosition = transform.position + firePoint;
    //     catrige.FireCatrige(FireLine.normalized, attackableEntityType, bulletGenerationPosition);
    //     _remainingAmmo -= 1;
    //     PlayFireSound();
    // }

    public void Fire(Catrige catrige, Entity.EntityType attackableEntityType = Entity.EntityType.Enemy)
    {
        if (!IsReadyToFire())
        {
            return;
        }
        float eulerAngle = transform.rotation.eulerAngles.z;
        Quaternion eulerAngleRotation = Quaternion.AngleAxis(eulerAngle, Vector3.forward);
        Vector3 FireLine =  eulerAngleRotation * Vector3.right;
        Vector3 bulletGenerationPosition = transform.position + eulerAngleRotation * _firePoint;
        catrige.FireCatrige(FireLine, attackableEntityType, bulletGenerationPosition);
        _remainingAmmo -= 1;
        PlayFireSound();
    }


    private void PlayFireSound()
    {
        if (_gunshotSound != null)
        {
            _audioSource.PlayOneShot(_gunshotSound);
        }
    }
    public void Reload()
    {
        if (IsReloading() || _remainingMagazine == 0)
        {
            return;
        }

        _reloadCoolDown = _reloadTime;
        _remainingAmmo = _magazineCapacity;
        _remainingMagazine -= 1;
    }
}
