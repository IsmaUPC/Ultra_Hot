using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public int multSpeed = 20;
    public GameObject hitParticlePrefab;
    public Rigidbody handR;
    public Rigidbody handL;

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
            BodyPartScript bp = collision.gameObject.GetComponent<BodyPartScript>();

            //if (!bp.enemy.dead)
            Instantiate(hitParticlePrefab, transform.position, transform.rotation);

            bp.HidePartAndReplace();
            bp.enemy.Ragdoll();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        Destroy(gameObject);
    }
}
