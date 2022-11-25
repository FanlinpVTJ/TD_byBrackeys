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
    [SerializeField] private float maxSpeed = 10f;
    
    private Waypoints waypoints;
    private Transform target;
    private float startSpeed; // TODO: _startSpeed
    private int wavepointIndex = 0; // TODO: _wayPointIndex

    private void Start()
    {
        target = waypoints.Points[0];
        startSpeed = maxSpeed;
        StartCoroutine(MoveToNextWayPoint());
    }

    public void SetWaypoints(Waypoints waypoints)
    {
        this.waypoints = waypoints;
    }
    public void ChangeSpeed(float _speedChange)
    {
        maxSpeed = startSpeed * (1 -_speedChange/100);
    }

    // TODO: почему Get? он же ничего не возвращает
    // просто MoveToNextWayPoint
    // TODO: почему Get? он же ничего не возвращает
    // это что-то вроде TrySetNewWayPoint 
    private void TrySetNewWayPoint()
    {
        // круто, что делаешь сразу return, а не if-else, зачет))
        if (wavepointIndex >= waypoints.Points.Length - 1)
        {
            EndPath();
            return;
        }
        wavepointIndex++;
        target = waypoints.Points[wavepointIndex];
        StartCoroutine(MoveToNextWayPoint());
    }

    private void EndPath()
    {
        Destroy(gameObject);
    }

    private IEnumerator MoveToNextWayPoint()
    {
        while (Vector3.Distance(transform.position, target.position) >= 0.3f)
        {
            var direction = target.position - transform.position;
            transform.Translate(direction.normalized * maxSpeed * Time.deltaTime, Space.World);
            maxSpeed = startSpeed;
            yield return null;
        }
        TrySetNewWayPoint();
        yield return null;
    }
}   
