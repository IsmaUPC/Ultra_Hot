using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Levitation : MonoBehaviour
{
    // Start is called before the first frame update
    public float speedLevi = 1;
    public float offsetTime = 0f;
    float angle = 10;
    Vector3 localPos;
    float localPosY;
    public float distance = 0.4f;
    private bool up = false;
    public bool randomOffsetTime = false;

    void Start()
    {
        localPos = transform.localPosition;
        localPosY = transform.localPosition.y;

        if (randomOffsetTime)
            offsetTime = Random.Range(0, 1.9f);
        Invoke("CheckOffsetTime", offsetTime);
    }

    void CheckOffsetTime()
    {
        up = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (up)
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
    public void DestroyLevitation()
    {
        Destroy(GetComponent<Levitation>());
    }
    public void ActiveGravity()
    {
        GetComponent<Rigidbody>().useGravity = true;
    }

}