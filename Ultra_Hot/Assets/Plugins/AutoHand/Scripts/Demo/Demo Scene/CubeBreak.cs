using UnityEngine;

namespace Autohand.Demo{
    public class CubeBreak : MonoBehaviour{
        [SerializeField] private GameObject enemiesLevel;
        public float force = 10f;
        [Header("Rotation")]
        private bool rot = true;
        public float rotateSpeed = 10;
        [Header("Levitation")]
        public float speedLevi = 1;
        float angle = 10;
        Vector3 localPos;
        float localPosY;
        public float distance = 0.1f;
        public bool up = true;

        Vector3[] offsets = { new Vector3(0.25f, 0.25f, 0.25f), new Vector3(-0.25f, 0.25f, 0.25f), new Vector3(0.25f, 0.25f, -0.25f), new Vector3(-0.25f, 0.25f, -0.25f),
                            new Vector3(0.25f, -0.25f, 0.25f), new Vector3(-0.25f, -0.25f, 0.25f), new Vector3(0.25f, -0.25f, -0.25f), new Vector3(-0.25f, -0.25f, -0.25f),};
        [ContextMenu("Break")]
        private void Start()
        {
            localPos = transform.localPosition;
            localPosY = transform.localPosition.y;
        }
        public void Break() {
            for(int i = 0; i < 8; i++) {
                var smallerCopy = Instantiate(gameObject, transform.position, transform.rotation);
                foreach(var joint in smallerCopy.GetComponents<FixedJoint>()) {
                    Destroy(joint);
                }
                try{
                    smallerCopy.transform.parent = transform;
                }
                catch { }
                smallerCopy.transform.localPosition += offsets[i];
                smallerCopy.transform.parent = null;
                smallerCopy.transform.localScale = transform.localScale/2f;
                smallerCopy.layer = LayerMask.NameToLayer(Hand.grabbableLayerNameDefault);
                smallerCopy.GetComponent<Rigidbody>().useGravity = false;
                Destroy(smallerCopy.GetComponent<CubeBreak>());
                var body = smallerCopy.GetComponent<Rigidbody>();
                body.ResetCenterOfMass();
                body.ResetInertiaTensor();
                body.velocity = GetComponent<Rigidbody>().velocity;
                body.AddRelativeForce(transform.rotation*(offsets[i]*force), ForceMode.Impulse);
                body.AddRelativeTorque(transform.rotation*(offsets[i]*force + Vector3.one*(Random.value/3f)), ForceMode.Impulse);
                body.mass /= 2;
                smallerCopy.GetComponent<Grabbable>().jointBreakForce /= 2;
                if(smallerCopy.transform.localScale.x < 0.03f)
                    smallerCopy.GetComponent<Grabbable>().singleHandOnly = true;
            }
            enemiesLevel.SetActive(true);

            Destroy(gameObject);
        }

        private void Update()
        {
            if (rot)
                transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.World);

            if (up)
            {
                localPos.y = distance * Mathf.Sin(angle) + localPosY;
                transform.localPosition = localPos;
                angle += Time.deltaTime * speedLevi;
            }
        }
        public void StopRotLev()
        {
            rot = false;
            up = false;
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
            if (other.gameObject.CompareTag("sword")){
                Break();
            }
        }

    }
}
