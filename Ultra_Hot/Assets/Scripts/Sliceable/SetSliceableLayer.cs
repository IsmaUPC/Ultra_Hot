using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSliceableLayer : MonoBehaviour
{
    public float dt= 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dt += Time.deltaTime;
        if (dt > 1)
        {
            gameObject.layer = 6;
            Destroy(this);
        }
    }
}
