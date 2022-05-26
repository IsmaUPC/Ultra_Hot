using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTime : MonoBehaviour
{
    public float speed;
    public Rigidbody handL;
    public Rigidbody handR;
    private Vector3 vecDir = Vector3.zero;
    private Rigidbody rb;
    public int multSpeed = 50;

    private void Start()
    {
        //lastPosition = transform.position;
        //rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

    }

    private void Update()
    {
        CalculateSpeed();
        //rb.velocity = rb.velocity.normalized * speed * multSpeed * Time.deltaTime;
        //rb.angularVelocity = rb.angularVelocity.normalized * speed * multSpeed * Time.deltaTime;
        transform.position += vecDir * speed * Time.deltaTime;
        if(vecDir != Vector3.zero)
        {
            //vecDir += new Vector3(0, Physics.gravity.y, 0) * Time.deltaTime*0.5f;
        }
        //lastPosition = transform.position;
    }

    void CalculateSpeed()
    {
        speed = handL.velocity.magnitude + handR.velocity.magnitude;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            vecDir = collision.relativeVelocity;
        }
    }
}