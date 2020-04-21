using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHandler : MonoBehaviour
{
    [SerializeField] private float yMin;

    void FixedUpdate()
    {
        if (transform.position.y < yMin)
            Destroy(gameObject);
    }
}
