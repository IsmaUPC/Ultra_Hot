using UnityEngine;
using DG.Tweening;

public class GunScript : MonoBehaviour
{
    private Rigidbody rb;
    private Collider coll;

    private BulletTime bulletTime;
    public GameObject hitParticlePrefab;

    [Space]
    [Header("Weapon Settings")]
    public int bulletAmount = 6;
    public Transform barrelTip;
    public GameObject bulletPrefab;
    public bool butNo = false;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        bulletTime = GameObject.FindGameObjectWithTag("Player").GetComponent<BulletTime>();

        if (transform.parent != null)
            rb.isKinematic = true;
    }

    public void Shoot(bool isEnemy)
    {
        if (bulletAmount <= 0)
            return;

        if (!isEnemy)
            bulletAmount--;
        else if (!butNo)
            transform.LookAt(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 0.4f, Camera.main.transform.position.z));

        GameObject bullet = Instantiate(bulletPrefab, barrelTip.position, transform.rotation);
        bullet.GetComponent<BulletScript>().GodMode = bulletTime.GodMode;

        if (GetComponentInChildren<ParticleSystem>() != null)
            GetComponentInChildren<ParticleSystem>().Play();

        gameObject.GetComponent<Animator>().SetTrigger("Shoot");
        gameObject.GetComponent<AudioSource>().Play();

        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.2f, .01f, 10, 90, false, true).SetUpdate(true);
    }

    public void Release()
    {
        transform.parent = null;
        rb.isKinematic = false;

        rb.AddForce((Camera.main.transform.position - transform.position) * 2, ForceMode.Impulse);
        rb.AddForce(Vector3.up * 2, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !transform.parent.CompareTag("Enemy") && collision.relativeVelocity.magnitude < 15)
        {
            Instantiate(hitParticlePrefab, transform.position, transform.rotation);

            collision.gameObject.GetComponentInParent<EnemyScript>().Ragdoll();
        }
    }
}
