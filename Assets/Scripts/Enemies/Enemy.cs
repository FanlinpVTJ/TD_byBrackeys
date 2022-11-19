using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;

    private Transform _target;
    private int wavepointIndex = 0;

    private void Start()
    {
        _target = Waypoints._points[0];
    }
    private void Update()
    {
        var _direction = _target.position - transform.position;
        transform.Translate(_direction.normalized * _speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, _target.position) <= 0.3f)
        {
            GetNextWaypoint();
        }
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
