using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTime : MonoBehaviour
{
    public float speed;
    public Rigidbody handL;
    public Rigidbody handR;

    private void Update()
    {
        speed = handL.velocity.magnitude + handR.velocity.magnitude * 20 + 0.3f;
        Time.timeScale = Mathf.InverseLerp(0,8,speed);
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
}