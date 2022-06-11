using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[SelectionBase]
public class EnemyScript : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;

    public bool dead;
    public bool movement;
    public Transform weaponHolder;

    private float distance;
    private float stoppedTime;
    private bool goPlayer;
    private Rigidbody rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(RandomAnimation());

        agent = GetComponent<NavMeshAgent>();
        if(movement)
            agent.SetDestination(Camera.main.transform.position);
        distance = Random.Range(0.5f, 5f);
        stoppedTime = 0;
        goPlayer = false;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!dead)
        {
            Vector3 newForward = (Camera.main.transform.position - transform.position).normalized;
            newForward = Quaternion.AngleAxis(-8.7f, Vector3.up) * newForward;
            rb.rotation = Quaternion.LookRotation(newForward, Vector3.up);
           
            if (movement)
            {
                if (stoppedTime <= 0)
                {
                    if (agent.remainingDistance <= 0.5f && goPlayer == true)
                        goPlayer = false;
                    if (agent.remainingDistance <= distance && goPlayer == false)
                        MovementIteration();
                }
                else
                    stoppedTime -= Time.deltaTime;
            }
        }
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

    void MovementIteration()
    {
        switch (Random.Range(1, 4))
        {
            case 1:
                agent.SetDestination(RandomNavSphere(transform.position, Random.Range(10, 30)));
                break;
            case 2:
                agent.SetDestination(Camera.main.transform.position);
                goPlayer = true;
                break;
            case 3:
                agent.SetDestination(transform.position);
                stoppedTime = Random.Range(2, 6);
                break;
            default:
                break;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, -1);

        return navHit.position;
    }
}