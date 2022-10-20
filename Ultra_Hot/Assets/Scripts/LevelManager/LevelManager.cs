using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Autohand
{
    public class LevelManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public TeleportManager tp;
        public FadeScreen fade;
        public List<GameObject> levels;
        public List<GameObject> dices;
        private int currentLevel = 0;
        private LevelDataTransfer levelData;

        private EnemyScript[] enemies;
        private int count = 0;
        private bool transition = false;

        void Start()
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("DataInfo");

            levelData = objs[0].GetComponent<LevelDataTransfer>();
            currentLevel = levelData.GetLevel();

            dices[currentLevel].SetActive(true);
            enemies = levels[currentLevel].GetComponentsInChildren<EnemyScript>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!transition)
            {
                foreach (var item in enemies)
                {
                    if (item.dead)
                        count++;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    count = enemies.Length;
                }
                // Last cube
                if (count == enemies.Length && currentLevel == levels.Count - 1)
                {
                    transition = true;
                    //currentLevel++;
                    levelData.ResetLevel();
                    dices[currentLevel + 1].SetActive(true);
                }
                else if (count == enemies.Length && currentLevel < levels.Count - 1)
                {
                    transition = true;
                    currentLevel++;
                    levelData.SetLevelPlus();

                    fade.FadeOut();
                    StartCoroutine(NextLevel());
                }
                count = 0;
            }

        }

        private IEnumerator NextLevel()
        {
            float timer = 0;
            while (timer <= fade.fadeDuration)
            {
                timer += Time.unscaledDeltaTime;
                yield return null;
            }

            foreach (var item in enemies)
            {
                item.gameObject.SetActive(false);
            }
            enemies = levels[currentLevel].GetComponentsInChildren<EnemyScript>();

            dices[currentLevel].SetActive(true);
            tp.ActiveTelepor();

            fade.FadeIn();
            transition = false;
        }
    }
}

