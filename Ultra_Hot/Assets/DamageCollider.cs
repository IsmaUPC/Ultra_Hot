using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Autohand
{
    public class DamageCollider : MonoBehaviour
    {
        private TeleportManager teleportM;
        private void Start()
        {
            teleportM = GameObject.Find("TeleportPointers").GetComponent<TeleportManager>();
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                
            }
        }
    }
}