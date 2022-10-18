using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceListener : MonoBehaviour
{
    public Slicer slicer;
    public Rigidbody rb;

    public GameObject hitParticlePrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Instantiate(hitParticlePrefab, transform.position, transform.rotation);

            other.gameObject.GetComponentInParent<EnemyScript>().Ragdoll();
        }

        slicer.isTouched = true;
    }
}
