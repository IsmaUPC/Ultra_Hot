using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Autohand
{
    public class LevelManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public TeleportManager tp;
        public List<GameObject> levels;
        public List<GameObject> dices;
        private int currentLevel = 0;
        private LevelDataTransfer levelData;

        private EnemyScript[] enemies;
        private PortalScript[] portals;
        private int count = 0;

        void Start()
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("DataInfo");

            levelData = objs[0].GetComponent<LevelDataTransfer>();
            currentLevel = levelData.GetLevel();

            dices[currentLevel].SetActive(true);
            enemies = levels[currentLevel].GetComponentsInChildren<EnemyScript>(true);
            portals = levels[currentLevel].GetComponentsInChildren<PortalScript>(true);
        }

        // Update is called once per frame
        void Update()
        {
            foreach (var item in enemies)
            {
                if (item.dead)
                    count++;
            }
            foreach (var item in portals)
            {
                if (!item.GetComponent<Animation>().isPlaying)
                    item.OnDestroy();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                count = enemies.Length;
            }

            if (count == enemies.Length && currentLevel == levels.Count)
            {
                dices[currentLevel].SetActive(true);
            }
            if (count == enemies.Length && currentLevel < levels.Count)
            {
                currentLevel++;
                levelData.SetLevelPlus();
                foreach (var item in enemies)
                {
                    item.gameObject.SetActive(false);
                }
                enemies = levels[currentLevel].GetComponentsInChildren<EnemyScript>();

                dices[currentLevel].SetActive(true);
                tp.ActiveTelepor();
            }
            count = 0;
        }
    }
}
    
