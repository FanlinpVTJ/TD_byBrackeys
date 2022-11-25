using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: найс, выглядит неплохо
// название +
public class CameraController : MonoBehaviour
{
    [SerializeField] private float panSpeed = 30f;
    [SerializeField] private float panBorderThiсkness = 10f; // TODO: ...Thickness - просто орфография, но я же должен до чего-то доебаться
    [SerializeField] private float scrollSpeed = 2f;
    [SerializeField] private float maxYScroll = 80f;
    [SerializeField] private float minYScroll = 10f;

    private bool isCameraMovementEnabled = false;
    private bool hasCameraReturnToStart = false;
    private Vector3 cameraStartPosition;
        
    // TODO: private
    private void Start()
    {
        cameraStartPosition = transform.position;
    }

    // TODO: private
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isCameraMovementEnabled)
        {
            isCameraMovementEnabled = true;
            hasCameraReturnToStart = false;
            
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && isCameraMovementEnabled)
        {
            isCameraMovementEnabled = false;
            hasCameraReturnToStart = true;
        } 

        if (!isCameraMovementEnabled && hasCameraReturnToStart)
        {
            transform.position = Vector3.Lerp(transform.position, cameraStartPosition, 0.1f);
        }

        // TODO: можно было бы вынести Translate за пределы if-ов, в конец, а в ифах только
        // moveDelta += Vector3.left или right и тд
        var moveDelta = Vector3.zero;
        if (isCameraMovementEnabled)
        {
            if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThiсkness)
            {
                moveDelta = Vector3.left * panSpeed;
            }
            if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThiсkness)
            {
                moveDelta = Vector3.right * panSpeed;
            }
            if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThiсkness)
            {
                moveDelta = Vector3.forward * panSpeed;
            }
            if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThiсkness)
            {
                moveDelta = Vector3.back * panSpeed;
            }
            transform.Translate(moveDelta * Time.deltaTime, Space.World);
            float _scroll = Input.GetAxis("Mouse ScrollWheel");
            Vector3 _currentPosition = transform.position;
            _currentPosition.y -= _scroll * 1000 * scrollSpeed * Time.deltaTime;
            _currentPosition.y = Mathf.Clamp(_currentPosition.y, minYScroll, maxYScroll);
            transform.position = _currentPosition;
        }
    }
}
