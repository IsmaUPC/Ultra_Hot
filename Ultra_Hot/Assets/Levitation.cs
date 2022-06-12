using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitation : MonoBehaviour
{
    // Start is called before the first frame update
    public float speedLevi = 1;
    float angle = 10;
    Vector3 localPos;
    float localPosY;
    public float distance = 0.4f;
    public bool up = true;

    void Start()
    {
        localPos = transform.localPosition;   
        localPosY = transform.localPosition.y;   
    }

    // Update is called once per frame
    void Update()
    {
        if(up)
        {
            localPos.y = distance * Mathf.Sin(angle) + localPosY;
            transform.localPosition = localPos;
            angle += Time.deltaTime * speedLevi;
        }        
    }

    public void StopLevitation()
    {
        up = false;
    }
}
