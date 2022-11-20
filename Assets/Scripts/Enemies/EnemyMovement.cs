using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 10f;

    private Transform _target;
    private float _startspeed;
    private int wavepointIndex = 0;

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

    private void GetMoveToPosition()
    {
        var _direction = _target.position - transform.position;
        transform.Translate(_direction.normalized * _maxSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, _target.position) <= 0.3f)
        {
            GetNextWaypoint();
        }
        _maxSpeed = _startspeed;
    }

    private void GetNextWaypoint()
    {
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
