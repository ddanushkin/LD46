using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRotationAndScale : MonoBehaviour
{
    [SerializeField] private Transform copyFrom;
    
    void Update()
    {
        transform.localScale = copyFrom.localScale;
        transform.rotation = copyFrom.rotation;
    }
}
