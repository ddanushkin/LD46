using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float xSpeed;
    [SerializeField] private float ySpeed;
    private Camera _camera;
    private Vector3 _checkY;

    void Start()
    {
        _camera = Camera.main;
        _checkY = _camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        if (transform.position.x < 0)
            xSpeed *= -1f;
    }
    void Update()
    {
        transform.Translate(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, 0f);
        // transform.Translate(
            // transform.position.y < 0f ? xSpeed * Time.deltaTime : 0f,
            // ySpeed * Time.deltaTime, 0f);
        if(transform.position.y < _checkY.y - 8f)
            Destroy(gameObject);
    }
}
