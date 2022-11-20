using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform _partToRotate;
    [SerializeField] private string _tagToFind = "Enemy";

    [SerializeField] private float _turretFireRange = 15f;
    [SerializeField] private float _speedRotationOfTurret = 15f;
    
    private ShootFromTurret shootFromTurret;
    private GameObject _target;
    private EnemyHealth _targetEnemyHealth;
    //private HashSet<GameObject> _targets = new HashSet<GameObject>();

    private void Start()
    {
        shootFromTurret = GetComponent<ShootFromTurret>(); 
        StartCoroutine(TargetingEnemy());
    }
    private void Update()
    {
        if (_target != null)
        {
            GetLookAtTarget();
            shootFromTurret.TargetSeek(_target);
            shootFromTurret._canTurretShoot = true;
            return;
        }
        shootFromTurret._canTurretShoot = false;
    }

    // TODO: если приучишь себя пользоваться гитом - не нужно будет оставлять закомменченный код
    // фуки-фу
    
    //private void OnTriggerEnter(Collider _enemy)
    //{
    //    if (_enemy.tag == "Enemy" && _onTrigerExitPermition)
    //    {
    //        foreach (var item in _targets)
    //        {
    //            if (_enemy.gameObject.GetHashCode() == item.gameObject.GetHashCode())
    //            {
    //                break;
    //            }
    //        }
    //        _targets.Add(_enemy.gameObject);
    //    }
    //}
    //private void OnTriggerExit(Collider _enemy)
    //{
    //    for (int i = 0; i < 5; i++)
    //    {
    //        if (_enemy.tag == "Enemy")
    //        {
    //            _targets.Remove(_enemy.gameObject);
    //            _onTrigerExitPermition = false;
    //        }
    //    }
    //}
    private void GetLookAtTarget()
    {
        var _targetRotation = Quaternion.LookRotation(_target.transform.position - _partToRotate.position);
        Quaternion _yrotationTurret = new Quaternion(0, _partToRotate.rotation.y, 0, _partToRotate.rotation.w);
        Quaternion _yTargetRotation = new Quaternion(0, _targetRotation.y, 0, _targetRotation.w);
        _partToRotate.rotation = Quaternion.Slerp(_yrotationTurret, _yTargetRotation, _speedRotationOfTurret * Time.deltaTime);
    }

    // TODO: а вот это как раз можно было в апдейте сделать)
    // оно непрерывное, у этого нет окончания
    // хотя судя по WaitForSeconds(0.2f), я так понимаю, тут такая задумка, что не каждый кадр проверки?
    // или это чтоб не лагало от поиска по тегу?))
    private IEnumerator TargetingEnemy()
    {
        while (true)
        {
            GameObject _nearestEnemy = null;
            var _shortestDistance = _turretFireRange;
            
            // TODO: оу май, поиск по тэгу))
            GameObject[] _targets = GameObject.FindGameObjectsWithTag(_tagToFind);
            foreach (var enemy in _targets)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < _shortestDistance)
                {
                    _shortestDistance = distanceToEnemy;
                    _nearestEnemy = enemy;
                }
            }
            if (_nearestEnemy != null && _shortestDistance <= _turretFireRange)
            {
                _target = _nearestEnemy.gameObject;
            }
            else
            {
                _target = null;
            }
            
            // Ну и если в апдейте, то это тут не надо
            yield return new WaitForSeconds(0.2f);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _turretFireRange);
    }
}
