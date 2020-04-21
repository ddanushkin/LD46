using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterShootingHandler : MonoBehaviour
{
    [SerializeField] private Transform bulletPlaceHolder;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float reloadSpeed;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float distance;
    [SerializeField] private string targetTag;
    [SerializeField] private AudioClip onBulletInstantiate;

    private bool _canShoot;
    private float _shotTimer;
    private Transform _target;

    [SerializeField] private AudioClip onAim;
    [SerializeField] private float aimingRepeatSpeed;
    [SerializeField] private float minAimPitch;
    [SerializeField] private float maxAimPitch;
    [SerializeField] private float aimPitchStep;
    private float _aimRepeatTimer;
    private float _aimPitch;
    
    [SerializeField] private ParticleSystem onShotParticles;
    
    private void Start()
    {
        _aimPitch = minAimPitch;
        _shotTimer = reloadSpeed;
        _aimRepeatTimer = aimingRepeatSpeed;
        _target = GameObject.FindWithTag(targetTag).transform;
        _canShoot = false;
    }
    
    void Update()
    {
        float distanceToTarget = Vector3.Distance(_target.position, transform.position);

        if (distanceToTarget <= distance)
        {
            if (_shotTimer > 0.0f)
            {
                _shotTimer -= Time.deltaTime;
                _aimRepeatTimer -= Time.deltaTime;
                if (_aimRepeatTimer <= 0.0f)
                {
                    SoundManager.PlaySound(onAim, manualPitch: _aimPitch);
                    _aimRepeatTimer = aimingRepeatSpeed;
                    if (_aimPitch < maxAimPitch)
                        _aimPitch += aimPitchStep;
                    if (_aimPitch > maxAimPitch)
                        _aimPitch = maxAimPitch;
                }
            }
            if (_shotTimer <= 0.0f)
                _canShoot = true;
        }
        else
        {
            _aimPitch = minAimPitch;
            _aimRepeatTimer = aimingRepeatSpeed;
            _shotTimer = reloadSpeed;
        }

        if (_canShoot)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletPlaceHolder.position, bulletPlaceHolder.rotation);
            BulletController bc = bullet.GetComponent<BulletController>();
            bc.speed = bulletSpeed;
            bc.target = _target;
            _canShoot = false;
            SoundManager.PlaySound(onBulletInstantiate, true);
            _shotTimer = reloadSpeed;
            _aimPitch = minAimPitch;
            _aimRepeatTimer = aimingRepeatSpeed;
            onShotParticles.Play();
        }
    }
}
