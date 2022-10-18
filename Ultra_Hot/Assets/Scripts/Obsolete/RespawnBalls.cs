using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBalls : MonoBehaviour
{
    public GameObject[] obj;
    public int height = 0;
    public Vector3 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in obj)
        {
            if (item.transform.position.y < height)
            {
                item.transform.position = spawnPos;
                item.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }
}
