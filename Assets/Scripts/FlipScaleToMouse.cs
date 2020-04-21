using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipScaleToMouse : MonoBehaviour
{
    private Camera _camera;
    private readonly Quaternion _leftTurn = Quaternion.Euler(new Vector2(0f, 180f));
    private readonly Quaternion _rightTurn = Quaternion.Euler(new Vector2(0f, 0f));

    private void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        Vector2 screenTouchPosition = Input.mousePosition;
        if (screenTouchPosition.x < Screen.width / 2f)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _leftTurn, 7f);
        else if (screenTouchPosition.x > Screen.width / 2f)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _rightTurn, 7f);
    }
}
