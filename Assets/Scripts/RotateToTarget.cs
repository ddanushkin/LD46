using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToTarget : MonoBehaviour
{
    [SerializeField] private string targetTag;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float distance;
    [SerializeField] private float smooth;
    [SerializeField] private float speed;
    
    private Transform _target;

    private void Start()
    {
        if (targetTag == "")
            _target = targetTransform;
        else
            _target = GameObject.FindWithTag(targetTag).transform;
    }

    void Update()
    {
        Vector3 objectPosition = transform.position;
        Vector3 targetPosition = _target.position;
        
        if (Vector2.Distance(targetPosition, objectPosition) > distance)
            return;
        
        float angle = Mathf.Atan2(targetPosition.y - objectPosition.y, targetPosition.x - objectPosition.x) * Mathf.Rad2Deg;
        if (targetPosition.x < objectPosition.x)
            angle = angle - 180f;

        Vector3 eulerAngles = transform.eulerAngles;
        transform.eulerAngles = new Vector3(eulerAngles.x, eulerAngles.y,
            Mathf.SmoothDampAngle(eulerAngles.z, angle, ref speed, smooth));
    }
}
