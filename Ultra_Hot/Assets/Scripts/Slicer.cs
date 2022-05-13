using UnityEngine;
using EzySlice;
public class Slicer : MonoBehaviour
{
    public Material materialAfterSlice;
    public LayerMask sliceMask;
    public bool isTouched;

    private void Update()
    {
        if (isTouched == true)
        {
            isTouched = false;

            Collider[] objectsToBeSliced = Physics.OverlapBox(transform.position, new Vector3(0.5f, 0.7f, 0.1f), transform.rotation, sliceMask);
            
            foreach (Collider objectToBeSliced in objectsToBeSliced)
            {
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

        obj.GetComponent<Rigidbody>().AddExplosionForce(100, obj.transform.position, 10);
    }

    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }
}
