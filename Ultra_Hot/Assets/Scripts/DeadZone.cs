using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    // Start is called before the first frame update
    public TransitionManager manager;
    private int firstTime = 0;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (firstTime == 0)
            {
                firstTime++;
                other.gameObject.GetComponent<BulletTime>().GodMode = true;
            }
            else if(firstTime == 1)
            {
                firstTime++;
                manager.GoToScene(1);
            }
        }
    }
}
