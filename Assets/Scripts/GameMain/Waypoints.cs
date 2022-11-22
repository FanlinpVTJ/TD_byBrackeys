using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: ну тут я думаю понятно, что можно было не делать статик и прокинуть просто ссылку на объект в мувмент)
// короче если про названия ничего плохого не напишу - значит + 
public class Waypoints : MonoBehaviour
{
    public static Transform[] _points;

    private void Awake()
    {
        _points = new Transform[transform.childCount];
        for (int i = 0; i < _points.Length; i++)
        {
           _points[i] = transform.GetChild(i);
        }
    }
}
