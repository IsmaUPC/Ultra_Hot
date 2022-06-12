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

    void Start()
    {
        localPos = transform.localPosition;   
        localPosY = transform.localPosition.y;   
    }

    // Update is called once per frame
    void Update()
    {
        localPos.y = 0.5f * Mathf.Sin(angle) + localPosY;
        transform.localPosition = localPos;
        angle += Time.deltaTime * speedLevi;
    }
}
