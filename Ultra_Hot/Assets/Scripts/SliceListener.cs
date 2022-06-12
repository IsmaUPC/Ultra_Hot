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
            BodyPartScript bp = other.gameObject.GetComponent<BodyPartScript>();

            if (bp.bodyPartPrefab != null)
            {
                Instantiate(hitParticlePrefab, transform.position, transform.rotation);

                bp.HidePartAndReplace();
            }
            bp.enemy.Ragdoll();
        }

        slicer.isTouched = true;
    }
}
