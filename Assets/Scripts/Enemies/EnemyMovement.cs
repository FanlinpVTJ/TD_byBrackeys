using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: можно было сделать корутину, которая перемещает из точки A в точку B,
// после чего вызывает TrySetNewWayPoint и так по кругу, пока точки не кончатся
// Update был бы не нужен, перемещение стало бы набором конечных действий, а не чем-то непрерывным
// Название зачот
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 10f;

    private Transform _target;
    private float _startspeed; // TODO: _startSpeed
    private int wavepointIndex = 0; // TODO: _wayPointIndex

    private void Start()
    {
        _target = Waypoints._points[0];
        _startspeed = _maxSpeed;
    }

    private void Update()
    {
        GetMoveToPosition();
    }

    public void ChangeSpeed(float _speedChange)
    {
        _maxSpeed = _startspeed * (1 -_speedChange/100);
    }

    // TODO: почему Get? он же ничего не возвращает
    // просто MoveToNextWayPoint
    private void GetMoveToPosition()
    {
        // TODO: direction без _
        var _direction = _target.position - transform.position;
        transform.Translate(_direction.normalized * _maxSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, _target.position) <= 0.3f)
        {
            GetNextWaypoint();
        }
        _maxSpeed = _startspeed;
    }

    // TODO: почему Get? он же ничего не возвращает
    // это что-то вроде TrySetNewWayPoint 
    private void GetNextWaypoint()
    {
        // круто, что делаешь сразу return, а не if-else, зачет))
        if (wavepointIndex >= Waypoints._points.Length - 1)
        {
            EndPath();
            
            return;
        }
        wavepointIndex++;
        _target = Waypoints._points[wavepointIndex];
    }

    private void EndPath()
    {
        Destroy(gameObject);
        PlayerStats._liveCount--;
    }
}   
