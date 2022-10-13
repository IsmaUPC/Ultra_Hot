using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Autohand.Demo
{
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
            if(randomOffsetTime)
                Invoke("CheckOffsetTime", Random.Range(0,0.9f));
            else
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
    }
}