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

    public Rigidbody handR;
    public Rigidbody handL;

    public GameObject hitParticlePrefab;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();

        if (transform.parent != null)
            rb.isKinematic = true;
    }

    public void Shoot(bool isEnemy)
    {
        if (bulletAmount <= 0)
            return;

        if (!isEnemy)
            bulletAmount--;
       
        GameObject bullet = Instantiate(bulletPrefab, barrelTip.position, transform.rotation);
        bullet.GetComponent<BulletScript>().handR = handR;
        bullet.GetComponent<BulletScript>().handL = handL;

        if (GetComponentInChildren<ParticleSystem>() != null)
            GetComponentInChildren<ParticleSystem>().Play();

        gameObject.GetComponent<Animator>().SetTrigger("Shoot");
        gameObject.GetComponent<AudioSource>().Play();

        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.2f, .01f, 10, 90, false, true).SetUpdate(true);
    }

    public void Release()
    {
        transform.parent = null;
        rb.isKinematic = false;

        rb.AddForce((Camera.main.transform.position - transform.position) * 2, ForceMode.Impulse);
        rb.AddForce(Vector3.up * 2, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && transform.parent != null && collision.relativeVelocity.magnitude < 15)
        {
            BodyPartScript bp = collision.gameObject.GetComponent<BodyPartScript>();

            Instantiate(hitParticlePrefab, transform.position, transform.rotation);

            bp.HidePartAndReplace();
            bp.enemy.Ragdoll();
        }
    }
}
