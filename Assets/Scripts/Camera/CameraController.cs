using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _panSpeed = 30f;
    [SerializeField] private float _panBorderThikness = 10f;
    [SerializeField] private float _cameraReturnTime = 1f;
    [SerializeField] private float _scrollSpeed = 2f;
    [SerializeField] private float _maxYScroll = 80f;
    [SerializeField] private float _minYScroll = 10f;

    private bool _cameraMovementEnabled = false;
    private Vector3 _cameraStartPosition;

    void Start()
    {
        _cameraStartPosition = transform.position;
    }

    void Update()
    {
        if (GameMain._gameOver)
        {
            _cameraMovementEnabled = false;
            StartCoroutine(RuturnCameraToStartPosition());
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !_cameraMovementEnabled)
        {
            _cameraMovementEnabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && _cameraMovementEnabled)
        {
            StartCoroutine(RuturnCameraToStartPosition());
            _cameraMovementEnabled = false;
        } 
        if (_cameraMovementEnabled)
        {
            GetCameraControll();
        }
    }
    private IEnumerator RuturnCameraToStartPosition()
    {
        float elapsedTime = 0;
        while (elapsedTime < _cameraReturnTime)
        {
            transform.position = Vector3.Lerp(transform.position, _cameraStartPosition, elapsedTime / _cameraReturnTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        transform.position = _cameraStartPosition;
        yield return null;
    }

    private void GetCameraControll()
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
