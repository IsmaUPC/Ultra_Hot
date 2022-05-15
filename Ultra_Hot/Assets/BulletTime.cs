using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTime : MonoBehaviour
{
    public Transform handL;
    private Vector3 lastPosL;
    private float handSpeedL;
    public Transform handR;
    private Vector3 lastPosR;
    private float handSpeedR;

    public float timeScale = 1;
    private float timeScaleTarget;
    //private float fixedDeltaTime;

    bool slowmo = false;
    float slowmoThreshold = 10;
    float elapsed;

    // Start is called before the first frame update
    void Start()
    {
        //fixedDeltaTime = Time.fixedDeltaTime;
        elapsed = Time.realtimeSinceStartup;
    }

    private void FixedUpdate()
    {
        timeScaleTarget = slowmo ? 0.001f : 1;
        Time.timeScale = Mathf.Lerp(Time.timeScale, timeScaleTarget, Time.deltaTime * 30);
        //Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }

    private void LateUpdate()
    {
        if(Time.realtimeSinceStartup - elapsed >= 0.1f)
        {
            elapsed = Time.realtimeSinceStartup;
            CalcSlowmo();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void CalcSlowmo()
    {
        handSpeedL = (handL.position - lastPosL).magnitude;
        lastPosL = handL.position;
        handSpeedR = (handR.position - lastPosR).magnitude;
        lastPosR = handR.position;

        slowmo = handSpeedL + handSpeedR < slowmoThreshold;
    }
}