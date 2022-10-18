using UnityEngine;

public class BulletTime : MonoBehaviour
{
    public float speed;
    public Rigidbody handL;
    public Rigidbody handR;
    private float minVel = 0.1f;

    public bool GodMode = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            GodMode = !GodMode;
        if(!GodMode)
        {
            speed = handL.velocity.magnitude + handR.velocity.magnitude * 10 + minVel;
            Time.timeScale = Mathf.InverseLerp(0, 8, speed);

            // 0.02f because fixedDeltaTime defaults to this value
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
        else
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
        }
    }
}