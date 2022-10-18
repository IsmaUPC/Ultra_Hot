using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float atracSpeed;
    private GameObject prism = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(prism != null && !prism.GetComponent<LevelData>().GetHasGrababble())
        {
            prism.transform.position = Vector3.Lerp(prism.transform.position, transform.position, atracSpeed * Time.deltaTime);
            prism.transform.rotation = Quaternion.Lerp(prism.transform.rotation, transform.rotation, atracSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Level" && prism == null)
        {
            prism = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == prism)
        {
            prism = null;
        }
    }
}
