using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BulletTimeScript : MonoBehaviour
{
    public static BulletTimeScript instance;

    public float charge;
    public bool action;
    public GameObject bullet;
    public Transform bulletSpawner;

    [Header("Weapon")]
    public GunScript weapon;
    public Transform weaponHolder;
    public LayerMask weaponLayer;

    [Space]
    [Header("Prefabs")]
    public GameObject hitParticlePrefab;
    public GameObject bulletPrefab;


    private void Awake()
    {
        instance = this;
        if (weaponHolder.GetComponentInChildren<GunScript>() != null)
            weapon = weaponHolder.GetComponentInChildren<GunScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        //if (canShoot)
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        StopCoroutine(ActionE(.03f));
        //        StartCoroutine(ActionE(.03f));
        //        if (weapon != null)
        //            weapon.Shoot();
        //    }
        //}
        //
        //if (Input.GetMouseButtonDown(1))
        //{
        //    StopCoroutine(ActionE(.4f));
        //    StartCoroutine(ActionE(.4f));
        //
        //    if(weapon != null)
        //    {
        //        weapon.Throw();
        //        weapon = null;
        //    }
        //}

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        float time = (x != 0 || y != 0) ? 1f : .03f;
        float lerpTime = (x != 0 || y != 0) ? .05f : .5f;

        time = action ? 1 : time;
        lerpTime = action ? .1f : lerpTime;

        Time.timeScale = Mathf.Lerp(Time.timeScale, time, lerpTime);
    }

    IEnumerator ActionE(float time)
    {
        action = true;
        yield return new WaitForSecondsRealtime(.06f);
        action = false;
    }
}
