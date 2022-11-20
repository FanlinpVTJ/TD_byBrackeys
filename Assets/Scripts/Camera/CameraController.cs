using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: найс, выглядит неплохо
// название +
public class CameraController : MonoBehaviour
{
    [SerializeField] private float _panSpeed = 30f;
    [SerializeField] private float _panBorderThikness = 10f; // TODO: ...Thickness - просто орфография, но я же должен до чего-то доебаться
    [SerializeField] private float _scrollSpeed = 2f;
    [SerializeField] private float _maxYScroll = 80f;
    [SerializeField] private float _minYScroll = 10f;

    private bool _cameraMovementEnabled = false;
    private bool _cameraReturnToStart = false;
    private Vector3 _cameraStartPosition;
        
    // TODO: private
    void Start()
    {
        _cameraStartPosition = transform.position;
    }

    // TODO: private
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !_cameraMovementEnabled)
        {
            _cameraMovementEnabled = true;
            _cameraReturnToStart = false;
            
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && _cameraMovementEnabled)
        {
            _cameraMovementEnabled = false;
            _cameraReturnToStart = true;
        } 

        if (!_cameraMovementEnabled && _cameraReturnToStart)
        {
            transform.position = Vector3.Lerp(transform.position, _cameraStartPosition, 0.1f);
        }

        // TODO: можно было бы вынести Translate за пределы if-ов, в конец, а в ифах только
        // moveDelta += Vector3.left или right и тд
        if (_cameraMovementEnabled)
        {
            if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - _panBorderThikness)
            {
                transform.Translate(Vector3.left * _panSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= _panBorderThikness)
            {
                transform.Translate(Vector3.right * _panSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - _panBorderThikness)
            {
                transform.Translate(Vector3.forward * _panSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= _panBorderThikness)
            {
                transform.Translate(Vector3.back * _panSpeed * Time.deltaTime, Space.World);
            }

            float _scroll = Input.GetAxis("Mouse ScrollWheel");
            Vector3 _currentPosition = transform.position;
            _currentPosition.y -= _scroll * 1000 * _scrollSpeed * Time.deltaTime;
            _currentPosition.y = Mathf.Clamp(_currentPosition.y, _minYScroll, _maxYScroll);
            transform.position = _currentPosition;
        }
    }
}
