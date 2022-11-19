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
    public bool _canTurretShoot = false;

    private Transform _currentTargetTransform;
    private float _fireCountdown;
    private bool _enablelaserEffect = true;
    private int test = 0;

    private void Start()
    {
        _fireCountdown = _fireRate;
        if (_useLaser)
        {
            StartCoroutine(LaserShoot());
            _laserDamageEffect.Stop();
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
    }

    public void TargetSeek(Transform _target)
    {
        _currentTargetTransform = _target;
    }

    private IEnumerator LaserShoot()
    {
        while (true)
        {
            if (_canTurretShoot)
            {
                _lineRenderer.enabled = true;
                _lineRenderer.SetPosition(0, _firePointTransform[0].position);
                _lineRenderer.SetPosition(1, _currentTargetTransform.position);
                _laserDamageEffect.transform.position = _currentTargetTransform.position;
                if (_enablelaserEffect)
                {
                    _laserDamageEffect.Play();
                    _enablelaserEffect = false;
                }
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                _laserDamageEffect.Stop();
                _lineRenderer.enabled = false;
                _enablelaserEffect = true;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    private IEnumerator ShootQueue()
    {
        while (true) 
        {
            if (_fireCountdown <= 0 && _canTurretShoot) 
            {
                Debug.Log("после");
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
