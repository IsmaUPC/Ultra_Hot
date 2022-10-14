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
        private LevelDataTransfer levelData;
        private bool firstTp = true;
        private void Start()
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("DataInfo");
            levelData = objs[0].GetComponent<LevelDataTransfer>();
            index = levelData.GetLevel();
        }

        private void Update()
        {
            if(firstTp)
            {
                firstTp = false;
                playerBody.SetPosition(teleports[index].position, teleports[index].rotation);
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
                levelData.level++;
                //playerBody.transform.SetPositionAndRotation(teleports[index].position, teleports[index].rotation);

                playerBody.SetPosition(teleports[index].position, teleports[index].rotation);

            }
        }
    }
}

