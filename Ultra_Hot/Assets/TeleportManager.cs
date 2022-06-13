using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Autohand { 
    
    public class TeleportManager : MonoBehaviour
    {
        public List<Transform> teleports;
        //private Transform[] teleports;

        public AutoHandPlayer playerBody;
        //public GameObject playerBody;
        private int index = 0;
        private void Start()
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                teleports.Add(gameObject.transform.GetChild(i));
                //teleports[i] = gameObject.transform.GetChild(i);
            }
            playerBody.SetPosition(teleports[index].position,teleports[index].rotation);

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                ActiveTelepor();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectedTeleport(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SelectedTeleport(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SelectedTeleport(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SelectedTeleport(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SelectedTeleport(4);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                SelectedTeleport(5);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                SelectedTeleport(6);
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                SelectedTeleport(7);
            }
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                SelectedTeleport(8);
            }
        }
        void SelectedTeleport(int newIndex)
        {
            index = newIndex;
            //playerBody.transform.SetPositionAndRotation(teleports[index].position, teleports[index].rotation);

            playerBody.SetPosition(teleports[index].position, teleports[index].rotation);

        }
        public void ActiveTelepor()
        {
            if (index+1 < gameObject.transform.childCount)
            {
                index++;
                //playerBody.transform.SetPositionAndRotation(teleports[index].position, teleports[index].rotation);

                playerBody.SetPosition(teleports[index].position, teleports[index].rotation);

            }
        }
    }
}

