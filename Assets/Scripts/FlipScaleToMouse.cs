using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipScaleToMouse : MonoBehaviour
{
    private float _smoothTime = 0.3f;
    void Update()
    {
        Vector2 screenTouchPosition = Input.mousePosition;
        if (screenTouchPosition.x < Screen.width / 2f)
            transform.eulerAngles = new Vector3(0, Mathf.SmoothDamp(transform.eulerAngles.y, 180f,
                ref _smoothTime, 0.2f, 10000f, Time.unscaledDeltaTime), 0);
        else if (screenTouchPosition.x > Screen.width / 2f)
            transform.eulerAngles = new Vector3(0, Mathf.SmoothDamp(transform.eulerAngles.y, 0f,
                ref _smoothTime, 0.2f, 10000f, Time.unscaledDeltaTime), 0);
    }
}
