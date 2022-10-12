using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDataTransfer : MonoBehaviour
{
    // Start is called before the first frame update
    private int level;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Level")
        {
            level = other.gameObject.GetComponent<LevelData>().level;
            SceneManager.LoadScene(1);
        }
    }

    public int GetLevel()
    {
        return level;
    }

}
