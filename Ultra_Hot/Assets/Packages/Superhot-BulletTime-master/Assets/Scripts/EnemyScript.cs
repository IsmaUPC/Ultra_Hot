using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class EnemyScript : MonoBehaviour
{
    Animator anim;
    public bool dead;
    public Transform weaponHolder;

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(RandomAnimation());
    }

    void Update()
    {
        if (!dead)
            transform.LookAt(new Vector3(Camera.main.transform.position.x, 2, Camera.main.transform.position.z));
    }

    public void Ragdoll()
    {
        anim.enabled = false;
        BodyPartScript[] parts = GetComponentsInChildren<BodyPartScript>();
        foreach (BodyPartScript bp in parts)
        {
            bp.rb.isKinematic = false;
            bp.rb.interpolation = RigidbodyInterpolation.Interpolate;
        }
        dead = true;

        if (weaponHolder.GetComponentInChildren<GunScript>() != null)
        {
            GunScript w = weaponHolder.GetComponentInChildren<GunScript>();
            w.Release();
        }
    }

    public void Shoot()
    {
        if (dead)
            return;

        if (weaponHolder.GetComponentInChildren<GunScript>() != null)
            weaponHolder.GetComponentInChildren<GunScript>().Shoot(true);
    }

    IEnumerator RandomAnimation()
    {
        anim.enabled = false;
        yield return new WaitForSecondsRealtime(Random.Range(.1f, .5f));
        anim.enabled = true;
    }
}
