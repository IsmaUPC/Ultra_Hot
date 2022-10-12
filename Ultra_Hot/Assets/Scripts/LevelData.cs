using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    // Start is called before the first frame update
    public int level = 0;
    private bool hasGrabbable = false;
    void Start()
    {
        
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
    }

    public bool GetHasGrababble()
    {
        return hasGrabbable;
    }
}
