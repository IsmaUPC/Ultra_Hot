using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDataTransfer : MonoBehaviour
{
    // Start is called before the first frame update
    private int level = 0;
    public TransitionManager transitionManager;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    private void Start()
    {
        transitionManager = GameObject.FindGameObjectsWithTag("Transition")[0].GetComponent<TransitionManager>();
    }
    private void Update()
    {
        if(transitionManager == null)
            transitionManager = GameObject.FindGameObjectsWithTag("Transition")[0].GetComponent<TransitionManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Level")
        {
            level = other.gameObject.GetComponent<LevelData>().level;
            if(level != -1)
                transitionManager.GoToScene(2);
            else
            {
                level = 0;
                transitionManager.GoToScene(1);
            }
        }
    }

    public int GetLevel()
    {
        return level;
    }
    public void SetLevelPlus()
    {
        level++;
    }
    public void ResetLevel()
    {
        level = 0;
    }
}
