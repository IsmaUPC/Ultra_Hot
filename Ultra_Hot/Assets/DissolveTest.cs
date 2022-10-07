using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveTest : MonoBehaviour
{
    [SerializeField]
    Material dissolveMaterial;

    [SerializeField]
    EnemyScript enemy;

    List<GameObject> deadEnemies = new List<GameObject>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            OnEnemyDied(enemy);

        foreach(var e in deadEnemies)
        {
            Material mat = e.GetComponent<SkinnedMeshRenderer>().materials[0];
            EnemyScript enemyScript = e.transform.parent.GetComponent<EnemyScript>();
            if (enemyScript.dissolveWeight > 1.0f)
            {
                for (int i = 0; i < enemy.gameObject.transform.childCount - 1; ++i)
                {
                    deadEnemies.Remove(enemyScript.gameObject.transform.GetChild(i).gameObject);
                }
                Destroy(enemyScript.gameObject);
                break;
            }


            //Material mat = enemy.GetComponent<SkinnedMeshRenderer>().material;
            enemyScript.dissolveWeight += Time.deltaTime * .07f;
            if (mat.HasFloat("_Weight"))
                mat.SetFloat("_Weight", enemyScript.dissolveWeight);
            else
                Debug.Log("_Weight doesnt exist");
        }

    }

    public void OnEnemyDied(EnemyScript enemy)
    {
        for (int i = 0; i < enemy.gameObject.transform.childCount - 1; ++i)
        {
            GameObject child = enemy.gameObject.transform.GetChild(i).gameObject;
            if (child.GetComponent<SkinnedMeshRenderer>() != null)
                child.GetComponent<SkinnedMeshRenderer>().material = dissolveMaterial;
            else if (child.GetComponent<MeshRenderer>() != null)
                child.GetComponent<MeshRenderer>().material = dissolveMaterial;

            deadEnemies.Add(child);
        }
    }
}
