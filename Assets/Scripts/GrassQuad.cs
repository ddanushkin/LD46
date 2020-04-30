using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassQuad : MonoBehaviour
{
    public float speed;
    private Vector2 _offset;
    private Material _material;
    
    private void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        _offset = _material.mainTextureOffset;
        _offset.y += speed / transform.localScale.y * Time.deltaTime;
        _material.mainTextureOffset = _offset;
    }
}
