using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace Autohand
{

    public class CubeStart : MonoBehaviour
    {

        [SerializeField] private GameObject enemiesLevel;
        private float force = 1f;
        Vector3[] offsets = { new Vector3(0.25f, 0.25f, 0.25f), new Vector3(-0.25f, 0.25f, 0.25f), new Vector3(0.25f, 0.25f, -0.25f), new Vector3(-0.25f, 0.25f, -0.25f),
                            new Vector3(0.25f, -0.25f, 0.25f), new Vector3(-0.25f, -0.25f, 0.25f), new Vector3(0.25f, -0.25f, -0.25f), new Vector3(-0.25f, -0.25f, -0.25f),};

        //-----------------
        Grabbable grab;
        bool thrown = false;
        float throwTime = 3;

        void Awake()
        {
            grab = GetComponent<Grabbable>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!thrown || grab == null)
                return;
            //----------------
            Break();
        }

        public void Break()
        {
            for (int i = 0; i < 8; i++)
            {
                var smallerCopy = Instantiate(gameObject, transform.position, transform.rotation);
                foreach (var joint in smallerCopy.GetComponents<FixedJoint>())
                {
                    Destroy(joint);
                }
                try
                {
                    smallerCopy.transform.parent = transform;
                }
                catch { }



                smallerCopy.transform.localPosition += offsets[i];
                smallerCopy.transform.parent = null;
                smallerCopy.transform.localScale = transform.localScale / 2f;
                smallerCopy.layer = LayerMask.NameToLayer(Hand.grabbableLayerNameDefault);
                Destroy(smallerCopy.GetComponent<CubeStart>());
                var body = smallerCopy.GetComponent<Rigidbody>();
                body.ResetCenterOfMass();
                body.ResetInertiaTensor();
                body.isKinematic= false;
                body.useGravity = true;
                body.velocity = GetComponent<Rigidbody>().velocity;
                body.AddRelativeForce(transform.rotation * (offsets[i] * force), ForceMode.Impulse);
                body.AddRelativeTorque(transform.rotation * (offsets[i] * force + Vector3.one * (Random.value / 3f)), ForceMode.Impulse);
                body.mass /= 2;
                smallerCopy.GetComponent<Grabbable>().jointBreakForce /= 2;
                if (smallerCopy.transform.localScale.x < 0.03f)
                    smallerCopy.GetComponent<Grabbable>().singleHandOnly = true;
            }
            enemiesLevel.SetActive(true);

            Destroy(gameObject);
        }

    }

}