using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCrusher : MonoBehaviour
{
    public GameObject hitParticlePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.relativeVelocity.magnitude < 15)
        {
            BodyPartScript bp = collision.gameObject.GetComponent<BodyPartScript>();

            if (bp.bodyPartPrefab != null)
            {
                Instantiate(hitParticlePrefab, transform.position, transform.rotation);

                bp.HidePartAndReplace();
            }
            bp.enemy.Ragdoll();
        }
    }
}
