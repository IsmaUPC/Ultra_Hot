using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTime : MonoBehaviour
{
    public float speed;
    public Rigidbody handL;
    public Rigidbody handR;
    private Vector3 lastPosition;
    private Rigidbody rb;
    public int multSpeed = 10;

    private void Start()
    {
        //lastPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

    }

    private void Update()
    {
        CalculateSpeed();
        rb.velocity = rb.velocity.normalized * speed * multSpeed * Time.deltaTime;
        rb.angularVelocity = rb.angularVelocity.normalized * speed * multSpeed * Time.deltaTime;
        //transform.position += (transform.position - lastPosition) * speed * Time.deltaTime;
        //lastPosition = transform.position;
    }

    void CalculateSpeed()
    {
        speed = handL.velocity.magnitude + handR.velocity.magnitude;
    }
}