using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAtDirectionWithSpeed : MonoBehaviour
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private float speed;
    
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
