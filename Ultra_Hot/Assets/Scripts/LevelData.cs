using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    // Start is called before the first frame update
    public int level = 0;
    private bool hasGrabbable = false;
    private Quaternion originalRotation;
    void Start()
    {
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGrabPrism()
    {
        hasGrabbable = true;
    }
    public void OnReleasePrism()
    {
        hasGrabbable = false;
        //Quaternion.Lerp(transform.rotation, originalRotation, Time.deltaTime * 40);
    }

    public bool GetHasGrababble()
    {
        return hasGrabbable;
    }
}
