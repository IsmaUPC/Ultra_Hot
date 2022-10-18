using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Autohand
{
    public class DamageCollider : MonoBehaviour
    {
        public TransitionManager transitionManager;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                transitionManager.GoToScene(1);
            }
        }
    }
}