using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletScript : MonoBehaviour
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
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet"))
        {
            Instantiate(hitParticlePrefab, transform.position, transform.rotation);
            if(collision.gameObject.GetComponentInParent<EnemyScript>())
                collision.gameObject.GetComponentInParent<EnemyScript>().Ragdoll();
        }

        Destroy(gameObject);
    }
}
