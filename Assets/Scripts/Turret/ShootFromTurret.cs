using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: нэйминг почти одобряю, по крайней мере по нему понятно, что он делает, это гуд
// но классы обычно называют существительными: 
// напр TurretShootController, хотя контроллерами тоже не стоит увлекаться
// TurretWeapon, например, тоже подойдет
// но то, что я сразу понял по названию, что оно делает - это прям збс, это важнее, чем существительные
// если не придумывается логичное существительное - похер, пиши, чтоб понятно было, что оно делает
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

    // TODO: забыл private
    void Update()
    {
        if (_fireCountdown <= 0)
        {
            _fireCountdown = _fireRate;
        }
        _fireCountdown -= Time.deltaTime;

        // TODO: хехе ООП такое: да, да, пошел я нахер
        if (_useLaser && _canTurretShoot)
        {
            DoLaserDamage();
            _currentTargetMovement.ChangeSpeed(_laserSlowingFactor);
        }
    }

    // TODO: а методы - это обычно глагол: SeekTarget
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
            // TODO: почему нельзя было вызывать LaserShoot оттуда, где меняется _canTurrentShoot?
            // можно было бы убрать while и этот if, тогда корутина была бы конечным действием,
            // которое вызывается в определенный момент
            if (_canTurretShoot)
            {
                _lineRenderer.enabled = true;
                _laserDamageLightEffect.enabled = true;
                _lineRenderer.SetPosition(0, _firePointTransform[0].position);
                //Траим костыли
                try
                {
                    // TODO: ух, try-catch тоже не оч хорошо на перфоманс действует, но это не так плохо, фиг с ним
                    // вообще какого хрена?) зачем оно тут?
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
                    // TODO: ты ловишь вообще все эксепшны, значит никогда не узнаешь ни о каких ошибках
                // это приведет к Undefined Behavior, когда оно почему-то делает какую-то херню и ни ошибок и нифига
                // и тебе придется ооочень долго разбираться, что тут пошло не так
                // если у тебя конкретные ексепшны происходят и ты только от них хочешь ихбавиться - ты можешь написать так
                // catch (NullReferenceException)
                // ловиться будут только нулрефы, остальное все еще будет ексепшнами
                // блоков catch может быть несколько
                // но вообще try-catch обычно юзают в крайних случаях
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
            // TODO: почему нельзя было вызывать просто эту корутину, когда countdown < 0 там, где он собсно и меняется?
            // зачем тут эта проверка и бесконечный while?
            // получатся тот же апдейт, только ты его в корутину запихал
            if (_fireCountdown <= 0 && _canTurretShoot) 
            {
                // В твоей отдельной корутине было бы только то, что внутри этого if
                
                // TODO: без _
                foreach (var _firePointTransform in _firePointTransform)
                {
                    // TODO: без _
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
