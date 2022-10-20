using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    bool scaling = false;
    bool endedScaling = false;
    float x, y, z;
    public GameObject enemy;

    [SerializeField] float xSpeed = 0.5f;
    [SerializeField] float ySpeed = 0.5f;
    [SerializeField] float lifetime = 1.0f;

    float count = 0;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.35f, 0.02f, 0.05f);
        x = transform.localScale.x;
        y = transform.localScale.y;
        z = transform.localScale.z;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Static"))
        {
            enemy.SetActive(true);

            if (count <= lifetime)
            {
                count += Time.deltaTime;
                //Debug.Log(count);
            }
            else
            {
                //anim.SetBool("exiting", true);
                anim.Play("Exiting");
            }
        }
    }

    public void OnDestroy()
    {
        Destroy(this);
    }

}
