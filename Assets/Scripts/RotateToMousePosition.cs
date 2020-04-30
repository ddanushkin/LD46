using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotateToMousePosition : MonoBehaviour
{
    private Camera _camera;
    private float _smoothTime = 0.3f;

    void Start()
    {
        _camera = Camera.main;
    }
    
    void Update()
    {
        Vector2 screenTouchPosition = Input.mousePosition;
        Vector3 targetPosition = _camera.ScreenToWorldPoint(screenTouchPosition);
        float angle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;
        
        if (screenTouchPosition.x < Screen.width / 2f)
            angle = -angle - 180f;

        Vector3 eulerAngles = transform.eulerAngles;
        transform.eulerAngles = new Vector3(eulerAngles.x, eulerAngles.y,
            Mathf.SmoothDampAngle(eulerAngles.z, angle, ref _smoothTime, 0.3f, 10000f, Time.unscaledDeltaTime));
    }
}
