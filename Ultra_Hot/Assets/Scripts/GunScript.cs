using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GunScript : MonoBehaviour
{
    private Rigidbody rb;
    private Collider coll;

    [Space]
    [Header("Weapon Settings")]
    public int bulletAmount = 6;
    public Transform barrelTip;
    public GameObject bulletPrefab;


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();

        ChangeSettings();
    }

    private void ChangeSettings()
    {
        if (transform.parent != null)
            return;

        //rb.isKinematic = (BulletTimeScript.instance.weapon == this) ? true : false;
        //rb.interpolation = (BulletTimeScript.instance.weapon == this) ? RigidbodyInterpolation.None : RigidbodyInterpolation.Interpolate;
        //coll.isTrigger = (BulletTimeScript.instance.weapon == this);
    }

    public void Shoot()
    {
        if (bulletAmount <= 0)
            return;

        bulletAmount--;

        //StopCoroutine(BulletTimeScript.instance.ActionE(.03f));
        //StartCoroutine(BulletTimeScript.instance.ActionE(.03f));

        Instantiate(bulletPrefab, barrelTip.position, transform.rotation);

        if (GetComponentInChildren<ParticleSystem>() != null)
            GetComponentInChildren<ParticleSystem>().Play();

        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.2f, .01f, 10, 90, false, true).SetUpdate(true);
    }
}
