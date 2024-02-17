using System.Collections;
using System.Collections.Generic;
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
    }

    public bool IsMagazineEmpty()
    {
        return _remainingAmmo == 0;
    }
    bool IsReloading()
    {
        return _reloadCoolDown != 0;
    }
    public void Fire(float angle, BulletScript bullet)
    {
        if (IsReloading())
        {
            return;
        }

        for (int i = 0; i < bullet._pelletCount; i++)
        {
            float randomErrorAngle = Random.Range(-bullet._spreadAngle / 2, bullet._spreadAngle / 2);
            Vector3 firePoint = Quaternion.AngleAxis(angle, Vector3.forward) * _firePoint;
            GameObject pellet = Instantiate(bullet._bulletPrefab, transform.position + firePoint, Quaternion.identity);
            pellet.GetComponent<BulletScript>().SetVelocity(bullet._speed, angle + randomErrorAngle);
            pellet.GetComponent<BulletScript>().Activate();
        }

        // TODO : decrease ammo
    }
    public void PlayFireSound()
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
    }
}
