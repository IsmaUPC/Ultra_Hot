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

    [HideInInspector] public bool dead = false;
    public enum Movement
    {
        None = 0,
        Waypoints = 1,
        IA = 2
    }
    public Movement movement;
    public Transform weaponHolder;

    public int startShooting = 1;
    public List<Transform> waypoints;
    private bool moving = false;
    private int wayPointer = 0;

    private float distance;
    private float stoppedTime;

    [HideInInspector] float dissolveWeight = 0.0f;
    [SerializeField] SkinnedMeshRenderer meshRenderer;

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
            if (waypoints.Count <= 0)
            {
                foreach (Transform points in transform.parent)
                {
                    if (points.CompareTag("Waypoints"))
                    {
                        foreach (Transform enemy in points.GetComponentInChildren<Transform>())
                        {
                            if (enemy.name == name)
                            {
                                waypoints.AddRange(enemy.GetComponentsInChildren<Transform>());
                                waypoints.RemoveAt(0);
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            //TODO: Delete only if
            if (waypoints.Count <= 0)
            {
                Debug.Log("Waypoints length of enemy "+ this.name + " is 0");
                //moving = false;
                //movement = Movement.None;
            }
            else
            {
                moving = true;
                agent.SetDestination(waypoints[wayPointer].position);
            }
        }
        else
            anim.SetFloat("velocity", 0);

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
        else
        {
            meshRenderer.sharedMaterial.SetFloat("_DissolveAmount", dissolveWeight);
            dissolveWeight += Time.deltaTime;
            // TODO:
            //if (dissolveWeight >= 1.01)
            //{
            //    Destroy(gameObject);
            //}
        }
    }

    public void Ragdoll()
    {
        anim.enabled = false;

        if (weaponHolder.GetComponentInChildren<GunScript>() != null)
        {
            GunScript w = weaponHolder.GetComponentInChildren<GunScript>();
            w.Release();
        }

        Rigidbody[] parts = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody bp in parts)
        {
            bp.isKinematic = false;
            bp.interpolation = RigidbodyInterpolation.Interpolate;
        }

        //transform.DetachChildren();

        dead = true;
        agent.enabled = false;

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
            {
                agent.SetDestination(waypoints[wayPointer].position);
            }
            else
            {
                moving = false;
                anim.SetFloat("velocity", 0);
            }
            if (wayPointer >= startShooting)
            {
                anim.SetBool("shoot", true);
                weaponHolder.GetComponentInChildren<GunScript>().canLook = true;
            }
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