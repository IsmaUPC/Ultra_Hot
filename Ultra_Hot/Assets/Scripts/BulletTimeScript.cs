using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BulletTimeScript : MonoBehaviour
{
    public static BulletTimeScript instance;
    public bool action;

    [Space]
    [Header("Controls")]
    public Rigidbody handL;
    public Rigidbody handR;
    public Rigidbody head;


    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        //if (head.velocity.magnitude != 0)
        //{
        //    StopCoroutine(ActionE(.03f));
        //    StartCoroutine(ActionE(.03f));
        //};

        float time = (handL.velocity.magnitude != 0 || handR.velocity.magnitude != 0) ? 1f : .03f;
        float lerpTime = (handL.velocity.magnitude != 0 || handR.velocity.magnitude != 0) ? .05f : .5f;

        time = action ? 1 : time;
        lerpTime = action ? .1f : lerpTime;

        Time.timeScale = Mathf.Lerp(Time.timeScale, time, lerpTime);
    }

    public IEnumerator ActionE(float time)
    {
        action = true;
        yield return new WaitForSecondsRealtime(.06f);
        action = false;
    }
}
