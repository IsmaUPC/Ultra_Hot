using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletCompleteScript : MonoBehaviour
{
    public float speed;
    public int multSpeed = 20;
    public GameObject hitParticlePrefab;

    public bool GodMode = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(hitParticlePrefab, transform.position, transform.rotation);

            collision.gameObject.GetComponent<EnemyScript>().Ragdoll();

            //Before
            BodyPartScript bp = collision.gameObject.GetComponent<BodyPartScript>();

            if (bp.bodyPartPrefab != null)
            {
                Instantiate(hitParticlePrefab, transform.position, transform.rotation);

                bp.HidePartAndReplace();
            }
            bp.enemy.Ragdoll();
        }

        if (collision.gameObject.CompareTag("Player") && !GodMode)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        Destroy(gameObject);
    }
}
