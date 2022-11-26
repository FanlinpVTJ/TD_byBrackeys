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
    public event Action<int> OnDeathChangeLives;

    [SerializeField] private int costPlayerLivesOnEndPath = 1;
    [SerializeField] private float maxSpeed = 10f;
    
    private Waypoints waypoints;
    private Transform target;
    private float startSpeed;
    private bool isSpeedChanged = false;
    private int wavepointIndex = 0;

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
        isSpeedChanged = true;
        maxSpeed = startSpeed * (1 -_speedChange/100);
    }

    private void TrySetNewWayPoint()
    {
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
        OnDeathChangeLives?.Invoke(costPlayerLivesOnEndPath);
        Destroy(gameObject);
    }

    private IEnumerator MoveToNextWayPoint()
    {
        float timeElapsed = 0;
        var currentTransfom = transform.position;
        var currentTimeDistance = Vector3.Distance(currentTransfom, target.position) / maxSpeed;
        while (timeElapsed < currentTimeDistance)
        {
            transform.position = Vector3.Lerp(currentTransfom, target.position, timeElapsed / currentTimeDistance);
            timeElapsed += (Time.deltaTime * maxSpeed/ startSpeed);
            maxSpeed = startSpeed;
            yield return null;
        }
        transform.position = target.position;
        TrySetNewWayPoint();
        yield return null;
    }
}   
