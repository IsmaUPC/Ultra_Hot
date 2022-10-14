using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CarouselTexture : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture[] textures;
    public Vector3[] scales;
    public float[] zNear;
    public FadeScreen fade;
    public float timeXtex = 2.0f;
    private float totalTime = 0;
    private Material material;
    private int index = 1;
    private bool fadeing = false;
    private bool finish = false;
    void Start()
    {
        fade.FadeOut();
        totalTime = -fade.fadeDuration;
        material = GetComponent<Renderer>().material;
        material.SetTexture("_MainTex", textures[0]);
        transform.localScale = scales[0];
        transform.position = new Vector3(transform.position.x, transform.position.y, zNear[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if(!finish)
        {
            totalTime += Time.deltaTime;
            if (totalTime > timeXtex && !fadeing)
            {
                fadeing = true;
                fade.FadeIn();
            }
            else if (totalTime > timeXtex + fade.fadeDuration)
            {
                material.SetTexture("_MainTex", textures[index]);
                transform.localScale = scales[index];
                transform.position = new Vector3(transform.position.x, transform.position.y, zNear[index]);
                index++;
                fade.FadeOut();
                totalTime = -fade.fadeDuration;
                fadeing = false;

                if (index == textures.Length)
                    finish = true;
            }
        }        
    }
}
