using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
[SelectionBase]
public class WeaponScript : MonoBehaviour
{
    [Header("Bools")]
    public bool active = true;

    private Rigidbody rb;
    private Collider collider;
    private Renderer renderer;

    [Space]
    [Header("Weapon Settings")]
    public int bulletAmount = 6;
    public Transform barrelTip;
    public GameObject bulletPrefab;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        renderer = GetComponent<Renderer>();

        ChangeSettings();
    }

    private void ChangeSettings()
    {
        if (transform.parent != null)
            return;

        rb.isKinematic = (SuperHotScript.instance.weapon == this) ? true : false;
        rb.interpolation = (SuperHotScript.instance.weapon == this) ? RigidbodyInterpolation.None : RigidbodyInterpolation.Interpolate;
        collider.isTrigger = (SuperHotScript.instance.weapon == this);
    }

    public void Shoot()
    {
        if (bulletAmount <= 0)
            return;

        bulletAmount--;

        Instantiate(bulletPrefab, barrelTip.position, transform.rotation);

        if (GetComponentInChildren<ParticleSystem>() != null)
            GetComponentInChildren<ParticleSystem>().Play();

        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.2f, .01f, 10, 90, false, true).SetUpdate(true);
    }
}
