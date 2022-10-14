using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Autohand
{

}
[SelectionBase]
public class EnemyScript : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;
    private Rigidbody rb;

    [HideInInspector] public bool dead;
    public enum Movement
    {
        None = 0,
        Waypoints = 1,
        IA = 2
    }
    public Movement movement;
    public Transform weaponHolder;

    public List<Transform> waypoints;
    private bool moving = false;
    private int wayPointer = 0;

    private float distance;
    private float stoppedTime;

    [HideInInspector] public float dissolveWeight = 0.0f;


    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(RandomAnimation());

        agent = GetComponent<NavMeshAgent>();
        if (movement == Movement.IA)
        {
            agent.SetDestination(Camera.main.transform.position);
            moving = true;
        }
        else if (movement == Movement.Waypoints)
        {
            agent.SetDestination(waypoints[wayPointer].position);
            moving = true;
        }

        distance = Random.Range(0.5f, 5f);
        stoppedTime = 0;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!dead)
        {
            if (moving)
            {
                Vector3 newForward = (waypoints[wayPointer].position - transform.position).normalized;
                newForward.y = 0;
                newForward = Quaternion.AngleAxis(-8.7f, Vector3.up) * newForward;
                rb.rotation = Quaternion.LookRotation(newForward, Vector3.up);

                /*
                if (movement == Movement.IA)
                {
                    if (stoppedTime <= 0)
                    {
                        if (agent.remainingDistance <= 0.5f && moving == true)
                            moving = false;
                        if (agent.remainingDistance <= distance && moving == false)
                            MovementIteration();
                    }
                    else
                        stoppedTime -= Time.deltaTime;
                }
                else
                */
                    WayPoints();
            }
            else
            {
                Vector3 newForward = (Camera.main.transform.position - transform.position).normalized;
                newForward.y = 0;
                newForward = Quaternion.AngleAxis(-8.7f, Vector3.up) * newForward;
                rb.rotation = Quaternion.LookRotation(newForward, Vector3.up);
            }
        }
    }

    public void Ragdoll()
    {
        anim.enabled = false;

        Rigidbody[] parts = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody bp in parts)
        {
            bp.isKinematic = false;
            bp.interpolation = RigidbodyInterpolation.Interpolate;
        }

        Rigidbody[] parts = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody bp in parts)
        {
            bp.isKinematic = false;
            bp.interpolation = RigidbodyInterpolation.Interpolate;
        }

        dead = true;
        agent.enabled = false;

        if (weaponHolder.GetComponentInChildren<GunScript>() != null)
        {
            GunScript w = weaponHolder.GetComponentInChildren<GunScript>();
            w.Release();
        }
    }

    public void Shoot()
    {
        if (dead || moving == false)
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
                moving = true;
                break;
            case 3:
                agent.SetDestination(transform.position);
                stoppedTime = Random.Range(2, 6);
                break;
            default:
                break;
        }
    }

    void WayPoints()
    {
        if (agent.remainingDistance <= 0.5f)
        {
            wayPointer++;
            if (wayPointer < waypoints.Count)
                agent.SetDestination(waypoints[wayPointer].position);
            else
                moving = false;
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