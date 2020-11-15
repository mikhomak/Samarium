using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed;
    [SerializeField] private float distanceToChange;

    private Vector3 posChange; 
    
    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        //if()
        transform.position += direction * speed;
    }
}
