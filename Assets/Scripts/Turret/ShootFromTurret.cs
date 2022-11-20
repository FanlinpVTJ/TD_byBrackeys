using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFromTurret : MonoBehaviour
{
    [SerializeField] private Transform[] _firePointTransform;
    
    [Header("Bullets (default)")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _fireRate = 0.2f;
    
    [Header("Bullets (Laser)")]
    [SerializeField] bool _useLaser = false;
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] ParticleSystem _laserDamageEffect;
    [SerializeField] Light _laserDamageLightEffect;
    [SerializeField] float _laserDamage;
    [SerializeField] float _laserSlowingFactor;
    
    [Header("Optional(SetFromTurret)")]
    public bool _canTurretShoot = false;
    
    private GameObject _target;
    private Transform _currentTargetTransform;
    private EnemyHealth _currentTargetEnemyHealth;
    private EnemyMovement _currentTargetMovement;
    private float _fireCountdown;
    private bool _enablelaserEffect = true;
    private float _currentDeltaTimeLaserDamage;

    private void Start()
    {
        _fireCountdown = _fireRate;
        if (_useLaser)
        {
            StartCoroutine(LaserShoot());
            _lineRenderer.enabled = false;
            
        }
        else
        {
            StartCoroutine(ShootQueue());
        }
    }

    void Update()
    {
        if (_fireCountdown <= 0)
        {
            _fireCountdown = _fireRate;
        }
        _fireCountdown -= Time.deltaTime;

        if (_useLaser && _canTurretShoot)
        {
            DoLaserDamage();
            _currentTargetMovement.ChangeSpeed(_laserSlowingFactor);
        }
    }

    public void TargetSeek(GameObject _target)
    {
        this._target = _target;
        _currentTargetTransform = _target.GetComponent<Transform>();
        _currentTargetEnemyHealth = _target.GetComponent<EnemyHealth>();
        _currentTargetMovement = _target.GetComponent<EnemyMovement>();
    }

    private IEnumerator LaserShoot()
    {
        while (true)
        {
            if (_canTurretShoot)
            {
                _lineRenderer.enabled = true;
                _laserDamageLightEffect.enabled = true;
                _lineRenderer.SetPosition(0, _firePointTransform[0].position);
                //Траим костыли
                try
                {
                    _lineRenderer.SetPosition(1, _currentTargetTransform.position);

                    Vector3 _damageEffectDirection = _firePointTransform[0].position -
                        _laserDamageEffect.transform.position;
                                                                    ;
                    Vector3 _damageEffectDirectionOffset = _damageEffectDirection.normalized * 1.1f;

                    _laserDamageEffect.transform.rotation = Quaternion.LookRotation(_damageEffectDirection);
                    _laserDamageEffect.transform.position = _currentTargetTransform.position + 
                        _damageEffectDirectionOffset;


                }
                catch (Exception)
                {
                    _laserDamageLightEffect.enabled = false;
                    _laserDamageEffect.Stop();
                    _lineRenderer.enabled = false;
                    _enablelaserEffect = true;
                }
                
                if (_enablelaserEffect)
                {
                    _laserDamageEffect.Play();
                    _enablelaserEffect = false;
                }
                yield return null;
            }
            else
            {
                _laserDamageEffect.Stop();
                _lineRenderer.enabled = false;
                _laserDamageLightEffect.enabled = false;
                _enablelaserEffect = true;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    private void DoLaserDamage()
    {
        _currentDeltaTimeLaserDamage = _laserDamage * Time.deltaTime;
        _currentTargetEnemyHealth.DealDamage(_currentDeltaTimeLaserDamage);
    }

    private IEnumerator ShootQueue()
    {
        while (true) 
        {
            if (_fireCountdown <= 0 && _canTurretShoot) 
            {
                foreach (var _firePointTransform in _firePointTransform)
                {
                    GameObject _bulletGameObject = Instantiate(_bullet, _firePointTransform);
                    BulletBehaviour _bulletBehaviour = _bulletGameObject.GetComponent<BulletBehaviour>();
                    if (_bulletBehaviour != null)
                        _bulletBehaviour.ShotBullet(_currentTargetTransform);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            yield return null;
        }
    }
}
