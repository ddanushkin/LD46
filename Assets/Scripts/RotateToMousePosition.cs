using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotateToMousePosition : MonoBehaviour
{
    private Camera _camera;
    
    void Start()
    {
        _camera = Camera.main;
    }
    
    void Update()
    {
        Vector2 screenTouchPosition = Input.mousePosition;
        Vector3 target = _camera.ScreenToWorldPoint(screenTouchPosition);
        Vector3 position = transform.position;

        float angleRad = Mathf.Atan2(target.y - position.y, target.x - position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad;

        Vector3 localScale = transform.localScale;
        if (screenTouchPosition.x < Screen.width / 2f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 180f, 0), 7f);
            localScale.y = -1f;
            localScale.x = -1f;
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, -angleDeg);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0f, 0), 7f);
            localScale.y = 1f;
            localScale.x = 1f;
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, angleDeg);
        }
        
        transform.localScale = localScale;
    }
}
