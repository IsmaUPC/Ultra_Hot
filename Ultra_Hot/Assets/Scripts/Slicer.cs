using UnityEngine;
using EzySlice;

public class Slicer : MonoBehaviour
{
    public Material materialAfterSlice;
    //public LayerMask sliceMask;
    [TagSelector]
    public string tagCut = "";

    public float CutTimer = 2.0f;
    public bool isTouched;
    private bool CanCut = true;

    private void Update()
    {
        if (isTouched == true)
        {
            isTouched = false;

            Collider[] objectsToBeSliced = Physics.OverlapBox(transform.position, new Vector3(0.1f, 0.7f, 0.1f), transform.rotation);
            
            foreach (Collider objectToBeSliced in objectsToBeSliced)
            {
                if (CanCut && objectToBeSliced.CompareTag(tagCut))
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
    }

    private void MakeItPhysical(GameObject obj)
    {
        obj.AddComponent<MeshCollider>().convex = true;
        obj.AddComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;

        obj.GetComponent<Rigidbody>().useGravity = false;
        obj.GetComponent<Rigidbody>().AddExplosionForce(100, obj.transform.position, 10);

        obj.tag = tagCut;
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
