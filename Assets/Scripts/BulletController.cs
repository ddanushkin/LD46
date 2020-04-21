using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Transform target;
    public float speed;
    private Vector3 _direction;
    [SerializeField] private float lifeTime;
    [SerializeField] private AudioClip onBulletDestroyClip;
    [SerializeField] private AudioClip onBulletBalloonClip;
    
    private void Start()
    {
        _direction = (target.position - transform.position).normalized;
        _direction.z = 0;
    }
    
    void Update()
    {
        if (lifeTime <= 0.0f)
            Destroy(transform.gameObject);
        lifeTime -= Time.deltaTime;
        transform.position += _direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("shuriken"))
        {
            SoundManager.PlaySound(onBulletDestroyClip, true);
            Destroy(gameObject);
        }
        else if (other.CompareTag("balloon"))
        {
            SoundManager.PlaySound(onBulletBalloonClip, true);
            other.GetComponent<HoleSpawner>().SpawnNewHole();
            Destroy(gameObject);
        }
    }
}