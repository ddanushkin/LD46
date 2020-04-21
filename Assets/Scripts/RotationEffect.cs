using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationEffect : MonoBehaviour
{
    [SerializeField] private float speed;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.z += speed * Time.deltaTime;
        transform.eulerAngles = rotation;
    }
}
