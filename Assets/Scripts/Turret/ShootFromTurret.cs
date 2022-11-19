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

    private Transform _currentTargetTransform;
    private float _fireCountdown;
    private bool _isTarget = false;
   
    
    void Update()
    {
        if (_useLaser)
        {
            _laserDamageEffect.transform.position = _currentTargetTransform.position;
        }
        
        if (_fireCountdown <= 0 && _isTarget)
        {
            if (_useLaser)
            {
                LaserShoot();
                _isTarget = false;
            }
            else
            {
                StartCoroutine(ShootQueue());
                _fireCountdown = _fireRate;
                _isTarget = false;
            }
        }
        else if (_lineRenderer.enabled)
        {
            _lineRenderer.enabled = false;
            _laserDamageEffect.Stop();
        }

        _fireCountdown -= Time.deltaTime;
    }

    public void TargetSeek(Transform _target)
    {
        _currentTargetTransform = _target;
        _isTarget = true;
    }
    private void LaserShoot()
    {
        if (!_lineRenderer.enabled && _isTarget) 
        {
            _lineRenderer.enabled = true;
            _laserDamageEffect.Play();
        }
        _lineRenderer.SetPosition(0, _firePointTransform[0].position);
        _lineRenderer.SetPosition(1, _currentTargetTransform.position);
    }

    private IEnumerator ShootQueue()
    {
        foreach (var _firePointTransform in _firePointTransform)
        {
            if (_currentTargetTransform != null)
            {
                GameObject _bulletGameObject = Instantiate(_bullet, _firePointTransform);
                BulletBehaviour _bulletBehaviour = _bulletGameObject.GetComponent<BulletBehaviour>();
                if (_bulletBehaviour != null)
                    _bulletBehaviour.ShotBullet(_currentTargetTransform);
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
}
