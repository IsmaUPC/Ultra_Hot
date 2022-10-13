using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDataTransfer : MonoBehaviour
{
    // Start is called before the first frame update
    private int level;
    public TransitionManager transitionManager;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Level")
        {
            level = other.gameObject.GetComponent<LevelData>().level;
            transitionManager.GoToScene(1);
        }
    }

    public int GetLevel()
    {
        return level;
    }

}
