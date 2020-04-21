using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToTarget : MonoBehaviour
{
    [SerializeField] private string targetTag;
    [SerializeField] private float distance;
    [SerializeField] private float _speed;
    
    private Vector3 defaultRotation;
    private Transform _target;

    private void Start()
    {
        _target = GameObject.FindWithTag(targetTag).transform;
        defaultRotation = Vector3.zero;
        if (_target.transform.position.x < transform.position.x)
        {
            Vector3 rotation = transform.eulerAngles;
            Vector3 scale = transform.localScale;
            rotation.z = 180;
            scale.x *= -1;
            scale.y *= -1;
            transform.eulerAngles = rotation;
            transform.localScale = scale;
            defaultRotation.z = 180f;
        }
    }

    void Update()
    {
        if (Vector3.Distance(_target.position, transform.position) > distance)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(defaultRotation), Time.deltaTime * 20f);
            return;
        }
        Quaternion rotationQuad = Quaternion.LookRotation(
            Vector3.forward,
            _target.position - transform.position);
        Vector3 rotationVec3 = rotationQuad.eulerAngles;
        rotationVec3.z += 90.0f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(rotationVec3), Time.deltaTime * _speed);
    }
}
