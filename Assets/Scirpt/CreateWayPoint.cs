using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WaypointsFree;
using static UnityEngine.GraphicsBuffer;

public class  CreateWayPoint : MonoBehaviour
{
    private  GameObject prev;
    private static GameObject Root;
    public void OnButtonClicked(GameObject point, GameObject p)
    {
        if (Root == null)
        {
            if (GameObject.Find("Parent(Clone)") != null)
            {
                Root = GameObject.Find("Parent(Clone)");
            }
            else
            {
                Root = Instantiate(p);
            }
        }

        if (prev == null)
        {
            prev = Instantiate(point, Root.transform.position + Vector3.forward * 1.5f, Quaternion.identity);
            prev.transform.parent = Root.transform;
        }
        else
        {
            Vector3 spawnPosition = prev.transform.position + prev.transform.forward * 1.5f;
            spawnPosition.y = 0f; // Ensure waypoint is on the ground
            prev = Instantiate(point, spawnPosition, Quaternion.identity);
            prev.transform.parent = Root.transform;
        }
    }


    public List<GameObject> GetWaypoint()
    {
        List<GameObject> list = new List<GameObject>();
        
        for (int i = 0; i < Root.transform.childCount; i++)
        {
            list.Add(Root.transform.GetChild(i).gameObject);
                
        }
       
        return list;
    }

}


