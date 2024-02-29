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

    public void Fire(Catrige catrige, float damageMultiplier = 1.0f, Entity.EntityType attackableEntityType = Entity.EntityType.Enemy)
    {
        if (!IsReadyToFire())
        {
            return;
        }
        float eulerAngle = transform.rotation.eulerAngles.z;
        Quaternion eulerAngleRotation = Quaternion.AngleAxis(eulerAngle, Vector3.forward);
        Vector3 FireLine = eulerAngleRotation * Vector3.right;
        Vector3 bulletGenerationPosition = transform.position + eulerAngleRotation * _firePoint;
        catrige.FireCatrige(FireLine, damageMultiplier, attackableEntityType, bulletGenerationPosition);
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
