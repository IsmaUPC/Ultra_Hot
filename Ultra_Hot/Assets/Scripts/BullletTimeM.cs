using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BullletTimeM : MonoBehaviour
{
    [SerializeField] private GameObject head;
    [SerializeField] private OVRInput.Controller rightHand;
    [SerializeField] private OVRInput.Controller LeftHand;
    private Vector3 headPositionLastFrame;

    private float sensitivity = 10;

    private float headMultiplier = 250;

    public float totalVTime;

    // Start is called before the first frame update
    void Start()
    {
        head.transform.position = headPositionLastFrame;
    }

    // Update is called once per frame
    void Update()
    {
        //Hands
        var rightHandVelocity = OVRInput.GetLocalControllerAngularVelocity(rightHand);
        var leftHandVelocity = OVRInput.GetLocalControllerAngularVelocity(LeftHand);
        totalVTime = rightHandVelocity.magnitude + leftHandVelocity.magnitude;

        //head
        var headDistanceFromLastFram = Vector3.Distance(head.transform.position,headPositionLastFrame);

        totalVTime += headDistanceFromLastFram * headMultiplier;

        totalVTime /= sensitivity;

        if (totalVTime >= 1)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = totalVTime;
        }


        headPositionLastFrame = head.transform.position;
    }

    
}
