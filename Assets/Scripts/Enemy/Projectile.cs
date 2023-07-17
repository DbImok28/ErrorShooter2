using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float movementSpeed = 0.05f;

    void Start()
    {
        Destroy(gameObject, 10);
    }

    void Update()
    {
        transform.position += movementSpeed * Time.deltaTime * transform.forward;
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    Destroy(gameObject);
    //}
}
