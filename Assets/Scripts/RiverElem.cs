using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RiverElem : MonoBehaviour
{
    public float speed;

    private Camera _camera;
    private float _finY;

    void Start()
    {
        _camera = Camera.main;
        _finY = _camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y - 2;
    }

    void Update()
    {
        transform.Translate(0, -speed * Time.deltaTime, 0);
        if (transform.position.y < _finY)
            Destroy(gameObject);
    }
}
