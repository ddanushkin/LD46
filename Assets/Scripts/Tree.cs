using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public float speed = 1f;
    private Camera _camera;
    private Vector3 _checkY;

    void Start()
    {
        _camera = Camera.main;
        _checkY = _camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
    }
    void Update()
    {
        transform.Translate(0, -speed * Time.deltaTime, 0);
        if(transform.position.y < _checkY.y - 8f)
            Destroy(gameObject);
    }
}
