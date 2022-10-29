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
        private bool firstTp = true;
        private void Start()
        {
            playerBody.useInterpolation = false;
            GameObject[] objs = GameObject.FindGameObjectsWithTag("DataInfo");
            index = objs[0].GetComponent<LevelDataTransfer>().GetLevel();
        }

        private void Update()
        {
            if(firstTp)
            {
                firstTp = false;
                StartCoroutine(SetStartPosition());
            }
        }
        private IEnumerator SetStartPosition()
        {
            yield return null;
            playerBody.useInterpolation = true;
            playerBody.SetPosition(teleports[index].position, teleports[index].rotation);
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

