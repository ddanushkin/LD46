using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorElem : MonoBehaviour
{
    public float speed = 1f;

    private Camera _camera;
    private float _finY;

    void Start()
    {
        _camera = Camera.main;
        _finY = _camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y - 2;
    }

    private void Update()
    {
        transform.Translate(0, -speed * Time.deltaTime, 0);
        if (transform.position.y < _finY)
            Destroy(this.gameObject);
    }
}
