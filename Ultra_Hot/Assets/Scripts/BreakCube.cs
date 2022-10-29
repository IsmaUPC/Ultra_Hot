using UnityEngine;
using UnityEngine.SceneManagement;

namespace Autohand.Demo
{
    public class BreakCube : MonoBehaviour
    {
        [SerializeField] private GameObject enemiesLevel;
        public float force = 10f;

        public bool lastCube = false;
        Vector3[] offsets = { new Vector3(0.25f, 0.25f, 0.25f), new Vector3(-0.25f, 0.25f, 0.25f), new Vector3(0.25f, 0.25f, -0.25f), new Vector3(-0.25f, 0.25f, -0.25f),
                            new Vector3(0.25f, -0.25f, 0.25f), new Vector3(-0.25f, -0.25f, 0.25f), new Vector3(0.25f, -0.25f, -0.25f), new Vector3(-0.25f, -0.25f, -0.25f),};
        [ContextMenu("Break")]
        private void Start()
        {
        }
        public void Break()
        {
            Destroy(GetComponent<Levitation>());
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
                smallerCopy.GetComponent<Rigidbody>().useGravity = false;
                Destroy(smallerCopy.GetComponent<BreakCube>());
                var body = smallerCopy.GetComponent<Rigidbody>();
                body.ResetCenterOfMass();
                body.ResetInertiaTensor();
                body.velocity = GetComponent<Rigidbody>().velocity;
                body.AddRelativeForce(transform.rotation * (offsets[i] * force), ForceMode.Impulse);
                body.AddRelativeTorque(transform.rotation * (offsets[i] * force + Vector3.one * (Random.value / 3f)), ForceMode.Impulse);
                body.mass /= 2;
                smallerCopy.GetComponent<Grabbable>().jointBreakForce /= 2;
                if (smallerCopy.transform.localScale.x < 0.03f)
                    smallerCopy.GetComponent<Grabbable>().singleHandOnly = true;
            }
            if (!lastCube)
            {
                enemiesLevel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(1);
            }

            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                Break();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("sword"))
            {
                Break();
            }
        }

    }
}
