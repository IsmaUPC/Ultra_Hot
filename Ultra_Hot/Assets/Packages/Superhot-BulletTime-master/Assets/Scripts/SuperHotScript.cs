using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SuperHotScript : MonoBehaviour
{

    public static SuperHotScript instance;

    public float charge;
    public bool action;
    public GameObject bullet;
    public Transform bulletSpawner;

    [Space]
    [Header("UI")]
    public Image indicator;

    [Space]
    [Header("Prefabs")]
    public GameObject hitParticlePrefab;
    public GameObject bulletPrefab;


    private void Awake()
    {
        instance = this;
        if (weaponHolder.GetComponentInChildren<WeaponScript>() != null)
            weapon = weaponHolder.GetComponentInChildren<WeaponScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        float time = (x != 0 || y != 0) ? 1f : .03f;
        float lerpTime = (x != 0 || y != 0) ? .05f : .5f;

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
