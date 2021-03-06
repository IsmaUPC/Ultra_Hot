using UnityEngine;
using EzySlice;

public class Slicer : MonoBehaviour
{
    public Material materialAfterSlice;
    public LayerMask layerCut;
    [TagSelector]

    public Rigidbody handL;
    public Rigidbody handR;

    public float CutTimer = 2.0f;
    public bool isTouched;
    private bool CanCut = true;

    private void Update()
    {
        if (isTouched == true)
        {
            isTouched = false;
            Collider[] objectsToBeSliced = Physics.OverlapBox(transform.position, new Vector3(0.1f, 0.7f, 0.1f), transform.rotation,layerCut);

            foreach (Collider objectToBeSliced in objectsToBeSliced)
            {
                CanCut = false;
                Invoke("Countdown", CutTimer);
                SlicedHull slicedObject = SliceObject(objectToBeSliced.gameObject, materialAfterSlice);

                GameObject upperHullGameobject = slicedObject.CreateUpperHull(objectToBeSliced.gameObject, materialAfterSlice);
                GameObject lowerHullGameobject = slicedObject.CreateLowerHull(objectToBeSliced.gameObject, materialAfterSlice);

                upperHullGameobject.transform.position = objectToBeSliced.transform.position;
                lowerHullGameobject.transform.position = objectToBeSliced.transform.position;

                MakeItPhysical(upperHullGameobject);
                MakeItPhysical(lowerHullGameobject);

                Destroy(objectToBeSliced.gameObject);
            }
        }
    }

    private void MakeItPhysical(GameObject obj)
    {
        obj.AddComponent<MeshCollider>().convex = true;
        obj.AddComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;

        obj.GetComponent<Rigidbody>().useGravity = false;
        obj.GetComponent<Rigidbody>().AddExplosionForce(100, obj.transform.position, 10);

        obj.AddComponent<BulletTime>().handL = handL;
        obj.GetComponent<BulletTime>().handR = handR;

        
        obj.layer = 13;
        Debug.LogWarning(obj.layer);
    }

    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }

    private void Countdown()
    {
        Debug.Log("CAN CUT");
        CanCut = true;
    }

}
